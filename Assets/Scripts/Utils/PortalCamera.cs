using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _portal;
    
    void Update()
    {
        Vector3 playerOffsetFromPortal = (_playerCamera.position - _portal.position).normalized;
        //Vector3 diff = _portal.position - playerOffsetFromPortal;
        transform.localPosition = playerOffsetFromPortal;// new Vector3(playerOffsetFromPortal.z, playerOffsetFromPortal.y, playerOffsetFromPortal.x);
        
        float angularDiff = Quaternion.Angle(_portal.rotation, Quaternion.Euler(0,90,0));
        Quaternion rotationalDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCamDiff = rotationalDiff * _playerCamera.forward;

        transform.rotation = Quaternion.LookRotation(newCamDiff, Vector3.up);
    }
}
