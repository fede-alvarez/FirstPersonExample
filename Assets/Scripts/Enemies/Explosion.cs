
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Transform _player;

    private void Start() 
    {
        _player = GameManager.GetInstance.GetPlayer;    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Player")) return;
        
        Vector3 forceDir = (_player.position - transform.position).normalized;
        _player.GetComponent<PlayerMovement>().Explode(forceDir);
    }
}
