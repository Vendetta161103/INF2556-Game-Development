using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField]float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator animator;

    private Rigidbody2D rb;
    private PlayerInputActions inputActions;
    private float horizontal;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Disable();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        horizontal = inputActions.Player.Move.ReadValue<Vector2>().x;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        
        animator.SetBool("isRunning", horizontal != 0);
        animator.SetBool("isJumping", rb.linearVelocityY > 0.1 && !isGrounded);
        animator.SetBool("isFalling", rb.linearVelocityY < -0.1 && !isGrounded);

        if (horizontal > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<float>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}