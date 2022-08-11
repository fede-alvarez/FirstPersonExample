using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Crosshair")]
    [SerializeField] private RectTransform _crosshairRect;
    [SerializeField] private Image[] _crosshairParts;
    [Header("Crosshair props")]
    [SerializeField] private Color _crosshairHitColor;
    [SerializeField] private float _crosshairHitTime = 0.2f;
    
    private Color _currentColor;

    private bool _playerAiming = false;

    private void Start() 
    {
      _currentColor = _crosshairParts[0].color;
      
      EventManager.HitDetected += ShowHit;
      EventManager.PlayerAim += CrosshairFocus;
      EventManager.PlayerNoAim += CrosshairNormal;
    }

    public void CrosshairFocus()
    {
      if (_playerAiming) return;
      _playerAiming = true;
      _crosshairRect.DOSizeDelta(new Vector2(64,64), 0.4f);
    }

    public void CrosshairNormal()
    {
      if (!_playerAiming) return;
      _playerAiming = false;
      _crosshairRect.DOSizeDelta(new Vector2(94,94), 0.4f);
    }

    private void SetColor(Color newColor)
    {
      foreach(Image imgPart in _crosshairParts)
      {
        imgPart.color = newColor;
      }
    }

    public void ShowHit()
    {
      CancelInvoke();
      SetColor(_crosshairHitColor);
      Invoke("ResetColor", _crosshairHitTime);
    }

    private void ResetColor()
    {
      SetColor(_currentColor);
    }

    private void OnDestroy() 
    {
      EventManager.HitDetected -= ShowHit;
      EventManager.PlayerAim -= CrosshairFocus;
      EventManager.PlayerNoAim -= CrosshairNormal;
    }
}
