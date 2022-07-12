using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour, IDamagable
{
    [SerializeField] private int _loopCounts = 5;
    [SerializeField] private float _loopDuration = 0.1f;

    private bool _shooted = false;
    private float _shootScale = 4.3f;

    private Vector3 _initialScale;
    private Quaternion _initialRotation;
    
    private void Start()
    {
        _initialScale = transform.localScale;
        _initialRotation = transform.localRotation;
    }

    public void Shooted()
    {
        if (_shooted) return;
        _shooted = true;

        EventManager.OnHitDetected();

        transform.DOScale(_shootScale, 0.2f).SetEase(Ease.InElastic).SetLoops(2, LoopType.Yoyo);
        transform.DOLocalRotate(new Vector3(0,1,0), _loopDuration).SetLoops(_loopCounts, LoopType.Incremental).SetEase(Ease.InSine).OnComplete(() => 
        {
            _shooted = false;
            transform.localRotation = _initialRotation;
        });
    }

    public void Damage(Vector3 damageDirection)
    {
        Shooted();
    }
}
