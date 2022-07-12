using UnityEngine;

public class Barrel : MonoBehaviour, IDamagable
{
    private Rigidbody _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Damage(Vector3 direction)
    {
        EventManager.OnHitDetected();
        
        _rb.isKinematic = false;
        _rb.AddForce(direction * 5, ForceMode.Impulse);
    }
}
