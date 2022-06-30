using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Main refs")]
    [SerializeField] private Transform _fpCamera;
    [SerializeField] private Transform _weaponHolder;
    
    [Header("Settings")]
    [SerializeField] private float _sensitivity = 1;
    [SerializeField] private float _smoothing = 1f;

    [Header("Gun Sway")]
    [SerializeField] private float _swayMultiplier = 1;

    private Vector2 _rotations = new Vector2(0,90);
    
    private Vector2 _frameVelocity;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() 
    {
        LookAtMouse();
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
        newRotations.x = Mathf.Clamp(newRotations.x, -50, 50);
        newRotations.y = Mathf.Clamp(newRotations.y, -50, 50);

        Quaternion xRotation = Quaternion.AngleAxis(newRotations.y, Vector3.right);
        Quaternion yRotation = Quaternion.AngleAxis(-newRotations.x, Vector3.up);

        Quaternion targetRotation = xRotation * yRotation;
        _weaponHolder.localRotation = Quaternion.Slerp(_weaponHolder.localRotation, targetRotation, _swayMultiplier * Time.deltaTime);
    }
}
