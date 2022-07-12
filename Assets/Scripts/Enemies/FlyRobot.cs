using UnityEngine;
using DG.Tweening;

public class FlyRobot : MonoBehaviour, IDamagable
{
    [SerializeField] protected float _wanderRadius = 8;
    [SerializeField] protected float _wanderTimer = 5;

    private float _timer;
    private bool _isAttacking = false;
    private Transform _player;
    private Rigidbody _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();        
    }

    private void Start() 
    {
        _player = GameManager.GetInstance.GetPlayer;    
    }

    private void Update() 
    {
        if (!_isAttacking)
            WanderAround();
        else
            Attack();
    }

    private void Attack()
    {
        _timer += Time.deltaTime;
        if (_timer >= 3) 
        {
            Vector3 playerDir = (_player.position - transform.position).normalized;
            transform.forward = playerDir;
            _rb.DOMove(playerDir * 5, 2.0f);

            _timer = 0;
        }
    }

    private void WanderAround()
    {
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer) 
        {
            Vector3 newDir = RandomNavSphere(transform.position, _wanderRadius);
            //transform.position = Vector3.Lerp(transform.position, transform.position + newDir, Time.deltaTime * 20);
            //_rb.MovePosition(transform.position + newDir);
            transform.forward = newDir.normalized;
            _rb.DOMove(newDir, 5.0f);
            _timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist) 
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        
        return randDirection;
    }

    public static float GetAngle(Vector3 origin, Vector3 target)
    {
        Vector3 direction = target - origin;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Player")) return;
        _isAttacking = true;
    }

    private void OnTriggerExit(Collider other) 
    {
        if (!other.CompareTag("Player")) return;
        _isAttacking = false;    
    }

    public void Damage(Vector3 dir)
    {
        _rb.AddForce(dir * 5, ForceMode.Impulse);
    }
}
