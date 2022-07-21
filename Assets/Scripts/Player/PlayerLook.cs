using UnityEngine;
using DG.Tweening;

public class PlayerLook : MonoBehaviour
{
    [Header("Main refs")]
    [SerializeField] private Transform _fpCamera;
    [SerializeField] private Transform _weaponHolder;
    
    [Header("Settings")]
    [SerializeField] private float _sensitivity = 1;
    [SerializeField] private float _smoothing = 1f;
    [SerializeField] private float _aimSpeed = 15;
    [SerializeField] private float _recoilSpeed = 25;

    [Header("Gun")]
    [SerializeField] private float _swayMultiplier = 1;
    
    private Vector2 _rotations = new Vector2(0,90);
    
    private Vector2 _frameVelocity;
    private Camera _fpCameraComponent;
    private bool _isAiming = false;
    private bool _isRecoiling = false;

    private void Awake() 
    {
        _fpCameraComponent = _fpCamera.GetComponent<Camera>();
    }

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() 
    {
        LookAtMouse();

        if (Input.GetMouseButton(1))
            Aim();
        else
            NoAim();
    }

    private void LateUpdate() 
    {
        SwayWeapon();
    }

    private void LookAtMouse()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * _sensitivity);
        
        _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, _smoothing * Time.deltaTime);

        // Up / Down
        _rotations.x -= _frameVelocity.y;
        _rotations.x = Mathf.Clamp(_rotations.x, -60, 60);

        // Sideways
        _rotations.y += _frameVelocity.x;

        Quaternion headRotation = Quaternion.AngleAxis(_rotations.x, Vector3.right);
        Quaternion bodyRotation = Quaternion.AngleAxis(_rotations.y, Vector3.up);

        _fpCamera.localRotation = headRotation;
        transform.localRotation = bodyRotation;
    }

    private void SwayWeapon()
    {
        // Weapon Sway
        Vector2 newRotations = _frameVelocity;
        newRotations.x = Mathf.Clamp(newRotations.x, -80, 80);
        newRotations.y = Mathf.Clamp(newRotations.y, -80, 80);

        Quaternion xRotation = Quaternion.AngleAxis(newRotations.y, Vector3.right);
        Quaternion yRotation = Quaternion.AngleAxis(-newRotations.x, Vector3.up);

        Quaternion targetRotation = xRotation * yRotation;
        _weaponHolder.localRotation = Quaternion.Slerp(_weaponHolder.localRotation, targetRotation, _swayMultiplier * Time.deltaTime);
    }

    private void Aim()
    {
        Vector3 newPos = _weaponHolder.localPosition;
        newPos.x = 0;

        _weaponHolder.localPosition = Vector3.Lerp(_weaponHolder.localPosition, newPos, Time.deltaTime * _aimSpeed);
        _fpCameraComponent.fieldOfView = Mathf.Lerp(_fpCameraComponent.fieldOfView, 45, Time.deltaTime * _aimSpeed);
    }

    private void NoAim()
    {
        Vector3 newPos = _weaponHolder.localPosition;
        newPos.x = 0.7f;

        _weaponHolder.localPosition = Vector3.Lerp(_weaponHolder.localPosition, newPos, Time.deltaTime * _aimSpeed);
        _fpCameraComponent.fieldOfView = Mathf.Lerp(_fpCameraComponent.fieldOfView, 60, Time.deltaTime * _aimSpeed);
    }

    public void Recoil()
    {
        if(_isRecoiling) return;
        _isRecoiling = true;
        _weaponHolder.DOLocalMoveZ(0.55f, 0.05f).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            _isRecoiling = false;
        });
    }
}
