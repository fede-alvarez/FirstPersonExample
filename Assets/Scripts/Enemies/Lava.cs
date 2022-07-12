using UnityEngine;
using DG.Tweening;

public class Lava : MonoBehaviour
{
    private bool _isLavaRising = false;
    
    void Start()
    {
        EventManager.LavaRised += OnLavaRaised;
    }

    private void OnLavaRaised()
    {
        if (_isLavaRising) return;
        _isLavaRising = true;

        transform.DOMoveY(transform.position.y + 1.6f, 4.0f).SetEase(Ease.Linear).OnComplete(() => 
        {
            _isLavaRising = false;
        });
    }

    private void OnDestroy() 
    {
        EventManager.LavaRised -= OnLavaRaised;
    }
}
