using UnityEngine;
using DG.Tweening;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private ParticleSystem _impactParticles;
    [SerializeField] private ParticleSystem _bulletHoleParticle;
    [SerializeField] private ParticleSystem _muzzleFlashParticles;
    [SerializeField] private LayerMask _enemiesMask;

    private PlayerLook _playerLook;
    private Vector2 _screenMiddle;

    private int _ammo = 99;
    private bool _noAmmo = false;

    private void Awake() 
    {
        _playerLook = GetComponent<PlayerLook>();
    }

    private void Start() 
    {
        _screenMiddle = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update() 
    {
        if (_noAmmo) return;
        if (!Input.GetMouseButtonDown(0)) return;
        
        BulletsCount();
        CheckCollision();

        _fpsCamera.DOShakePosition(0.08f, .1f, 24, 45);

        _muzzleFlashParticles.Play();
        _playerLook.Recoil();
        Invoke("HideMuzzleFlash", 0.1f);
    }

    private void BulletsCount()
    {
        _ammo--;
        if (_ammo <= 0)
        {
            _ammo = 0;
            _noAmmo = true;
        }

        EventManager.OnPlayerShooted(_ammo);
    }

    private void HideMuzzleFlash()
    {
        _muzzleFlashParticles.Stop();
    }

    private void CheckCollision()
    {
        Transform pointingTransform = GetFromRay();
        if (pointingTransform == null) return;
        
        Vector3 shootDirection = (pointingTransform.position - transform.position).normalized;
        
        /*
        if (pointingTransform.TryGetComponent(out Target target))
            target.Shooted();

        if (pointingTransform.TryGetComponent(out Barrel barrel))
            barrel.Damage(Vector3.zero);
        */

        if (pointingTransform.TryGetComponent(out IDamagable target))
        {
            target.Damage(shootDirection);
        }
    }

    private void SpawnParticles(RaycastHit hit)
    {
        Quaternion rotation = Quaternion.LookRotation(hit.normal);
        Instantiate(_impactParticles, hit.point, rotation);
        
        ParticleSystem go = Instantiate(_bulletHoleParticle, hit.point, rotation); 
        if (go != null) go.transform.parent = hit.transform;
    }

    private Transform GetFromRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_screenMiddle);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _enemiesMask)) 
        {
            if (hit.transform != null)
            {
                SpawnParticles(hit);
                return hit.transform;
            }
        }

        return null;
    }
}
