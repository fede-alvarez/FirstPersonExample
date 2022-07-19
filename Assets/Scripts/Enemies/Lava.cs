using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Player")) return;
        SceneManager.LoadScene(0);
    }

    private void OnDestroy() 
    {
        EventManager.LavaRised -= OnLavaRaised;
    }
}
