using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private float _rotationAngle;
    
    void Update()
    {
        _rotationAngle += _rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, _rotationAngle, 0);
    }
}
