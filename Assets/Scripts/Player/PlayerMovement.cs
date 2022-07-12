
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    [Header("Game Feel")]
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private float _jumpBufferTime = 0.1f;

    [Header("Gravity")]
    [SerializeField] private float _gravityScale = 1;
    [SerializeField] private float _fallGravityScale = 1;
    

    private bool _isGrounded = false;
    private bool _isJumpPressed = false;
    private bool _isJumpReleased = false;
    private bool _isRunPressed = false;
    private bool _isJumping = false;

    private float _gravity = Mathf.Abs(Physics.gravity.y);
    private float _coyoteTimeCounter = 0;
    private float _jumpBufferCounter = 0;
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

        bool jumpPressed  = Input.GetButtonDown("Jump");
        bool jumpReleased = Input.GetButtonUp("Jump");
        bool runPressed   = Input.GetButtonDown("Fire3");
        bool runReleased  = Input.GetButtonUp("Fire3");

        if (jumpPressed) 
        {
            _jumpBufferCounter = _jumpBufferTime;
        }else{
            _jumpBufferCounter -= Time.deltaTime;
        }

        _currentScale = (_isJumping && _rb.velocity.y < 0) ? _fallGravityScale : _gravityScale;

        if (_jumpBufferCounter > 0 && (_isGrounded || _coyoteTimeCounter < _coyoteTime)) 
        {
            _isJumpPressed = true;
            _jumpBufferCounter = 0;
        }

        if (_isGrounded)
        {
            _isJumping = false;
                
            if (runPressed) 
                _isRunPressed = true;
            else if (runReleased) 
                _isRunPressed = false;

            _coyoteTimeCounter = 0;
        }else{
            _coyoteTimeCounter += Time.deltaTime;

            if (jumpReleased && _rb.velocity.y > 0)
                _isJumpReleased = true;
        }
    }

    private void FixedUpdate() 
    {
        Vector3 moveDirection = transform.right * _movement.x + transform.forward * _movement.z;
        Vector3 rbVelocity = moveDirection.normalized * ((_isRunPressed) ? _runSpeed : _movementSpeed);

        rbVelocity.y = _rb.velocity.y;
        _rb.velocity = rbVelocity;

        if (_isJumpPressed)
        {
            _isJumpPressed = false;
            _isJumping = true;

            _rb.AddForce(transform.up * 100 * _jumpForce, ForceMode.Impulse);
        }

        if (_isJumpReleased)
        {
            _isJumpReleased = false;
            _rb.AddForce(-transform.up * 100 * (_jumpForce * 0.3f), ForceMode.Impulse);
        }

        // Set gravity
        _rb.AddForce(-transform.up * 100 * (_gravity * _currentScale), ForceMode.Force);
    }

    public void Explode(Vector3 direction)
    {
        _rb.AddForce(direction * 300, ForceMode.Impulse);
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
