using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Controller
{
    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float groundStickForce = -2f;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f;

    private CharacterController _characterController;
    private Vector3 _velocity;

    private bool IsGrounded { get; set; } = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleGroundCheck();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public override void HandleInputs()
    {
        HandleMovement(InputManager.Instance.KeyboardInput);
        HandleJump();
    }
    
    public override void Move()
    {
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }

    public void MoveTowards(Vector3 direction)
    {
        _characterController.Move(direction);
    }

    private void HandleGroundCheck()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);

        if (IsGrounded && _velocity.y < 0f)
        {
            _velocity.y = groundStickForce;
        }
    }

    private void HandleMovement(Vector2 keyboardInput)
    {
        Vector3 moveDirection = (transform.forward * keyboardInput.y + transform.right * keyboardInput.x) * movementSpeed;
        _velocity.x = moveDirection.x;
        _velocity.z = moveDirection.z;
    }

    private void HandleJump()
    {
        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.fixedDeltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}