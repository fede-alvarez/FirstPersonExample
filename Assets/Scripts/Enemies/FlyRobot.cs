using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class FlyRobot : MonoBehaviour, IDamagable
{
    public enum States {
        Wander,
        Search,
        Wait,
        Attack
    }

    [SerializeField] protected States _currentState = States.Wander;

    [SerializeField] protected LayerMask _floorMask;

    [SerializeField] protected float _wanderRadius = 8;
    [SerializeField] protected float _wanderTimer = 5;

    private float _timer;
    private bool _isAttacking = false;
    private Transform _player;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();        
    }

    private void Start() 
    {
        _player = GameManager.GetInstance.GetPlayer;    
    }

    private void Update() 
    {
        switch (_currentState)
        {
            case States.Wander:
                WanderAround();
                break;
                
            case States.Wait:
                Wait();
                break;

            case States.Attack:
                Attack();
                break;
        }
    }

    private void WanderAround()
    {
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer) 
        {
            _timer = 0;

            Vector3 newDir = RandomNavSphere(transform.position, _wanderRadius, _floorMask);
            transform.forward = newDir.normalized;
            
            _agent.destination = newDir;
        }
    }

    private void Wait()
    {
        _timer += Time.deltaTime;

        if (_timer >= 2) 
        {
            _timer = 0;
            SetState(States.Attack);
        }
    }

    private void Attack()
    {
        _agent.destination = _player.position;
    }

    private void Search()
    {
        _agent.destination = new Vector3(0,0,0);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Player")) return;
        SetState(States.Wait);
    }

    private void OnTriggerExit(Collider other) 
    {
        if (!other.CompareTag("Player")) return;
        SetState(States.Wander);
    }

    public void Damage(Vector3 dir)
    {
        _rb.AddForce(dir * 5, ForceMode.Impulse);
    }

    public void SetState(States state)
    {
        _currentState = state;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, LayerMask mask) 
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, dist, mask);
        return navHit.position;
    }
}
