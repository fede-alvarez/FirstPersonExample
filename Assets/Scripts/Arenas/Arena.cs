using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private bool _canRaise = true;
    private bool _lavaRaised = false;

    private void OnTriggerEnter(Collider other) 
    {
        
        if (!_canRaise || _lavaRaised || !other.CompareTag("Player")) return;
        _lavaRaised = true;
        EventManager.OnLavaRised();
    }
}
