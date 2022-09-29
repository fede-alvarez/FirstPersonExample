using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private bool oneUseOnly = false;
    [SerializeField] private bool startDisabled = false;

    [Header("Events")]
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;
    
    private bool disabled = false;

    private void Awake() 
    {
        if (TryGetComponent(out MeshRenderer renderer))
            renderer.enabled = false;

        if (startDisabled) 
        {
            disabled = true;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.gameObject.CompareTag("Player") || disabled) return;
        onEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other) 
    {
        if (!other.gameObject.CompareTag("Player") || disabled) return;
        onExit?.Invoke();

        if (oneUseOnly)
            gameObject.SetActive(false);
    }

    public void SetActive()
    {
        disabled = false;
    }

    public void OnLevelLoaded()
    {
        onExit?.Invoke();

        if (oneUseOnly)
            gameObject.SetActive(false);
    }
}
