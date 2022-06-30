using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _crosshair;
    [SerializeField] private Color _crosshairHitColor;
    [SerializeField] private float _crosshairHitTime = 0.2f;

    private Color _currentColor;

    private void Start() 
    {
      _currentColor = _crosshair.color;
      EventManager.HitDetected += ShowHit;
    }

    public void ShowHit()
    {
      CancelInvoke();
      _crosshair.color = _crosshairHitColor;
      Invoke("ResetColor", _crosshairHitTime);
    }

    private void ResetColor()
    {
      _crosshair.color = _currentColor;
    }

    private void OnDestroy() 
    {
      EventManager.HitDetected -= ShowHit;
    }
}
