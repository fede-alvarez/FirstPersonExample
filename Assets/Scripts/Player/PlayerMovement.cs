
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    [Header("Gravity")]
    [SerializeField] private float _gravityScale = 1;
    [SerializeField] private float _fallGravityScale = 1;
    

    private bool _isGrounded = false;
    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private float _gravity = Mathf.Abs(Physics.gravity.y);
    private float _currentScale;
    
    private Vector3 _movement;
    private Rigidbody _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        bool jumpPressed = Input.GetButtonDown("Jump");
        
        if (jumpPressed && _isGrounded)
            _isJumpPressed = true;

         _currentScale = (_isJumping && _rb.velocity.y < 0) ? _fallGravityScale : _gravityScale;

        if (_isGrounded) _isJumping = false;
    }

    private void FixedUpdate() 
    {
        Vector3 dir = transform.right * _movement.x + transform.forward * _movement.z;
        Vector3 rbVelocity = dir.normalized * _movementSpeed;
        rbVelocity.y = _rb.velocity.y;
        
        _rb.velocity = rbVelocity;

        if (_isJumpPressed)
        {
            _isJumpPressed = false;
            
            _rb.AddForce(transform.up * 100 * _jumpForce, ForceMode.Impulse);
            _isJumping = true;
        }

        // Set gravity
        _rb.AddForce(-transform.up * 100 * (_gravity * _currentScale), ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Floor")) return;
        _isGrounded = true;
    }

    private void OnTriggerExit(Collider other) 
    {
        if (!other.CompareTag("Floor")) return;
        _isGrounded = false;
    }
}
