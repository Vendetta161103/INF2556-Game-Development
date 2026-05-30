using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float gravity = -20f;
    [SerializeField] float mouseSensitivity = 0.2f;
    [SerializeField] float verticalClamp = 80f;
    [SerializeField] Transform cameraHolder;
    [SerializeField] Transform foot;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip stompSound;

    private CharacterController controller;
    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private Vector3 velocity;
    private float sprintSpeed;
    private bool isGrounded;
    private int jumpsLeft = 1;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private bool wasInAir = false;

    public Transform GetFoot(){
        return foot;
    }

    public int GetJumpsLeft(){
        return jumpsLeft;
    }

    void Awake(){
        sprintSpeed = moveSpeed * 1.5f;
        controller = GetComponent<CharacterController>();
        inputActions = new InputSystem_Actions();

        // Maus im Spielfenster verstecken & sperren
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable(){
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += OnJump;
    }

    void OnDisable(){
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Disable();
    }

    void Update(){
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();

        yRotation += lookInput.x * mouseSensitivity;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        xRotation -= lookInput.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        bool isSprinting = inputActions.Player.Sprint.IsPressed();
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float speed = new Vector3(moveInput.x, 0, moveInput.y).magnitude * currentSpeed;
        Debug.Log("Speed: " + speed);
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsGrounded", isGrounded);

        if (!isGrounded){
            wasInAir = true;
        }
        
        if (isGrounded && wasInAir && jumpsLeft > 0){
            audioSource.PlayOneShot(stompSound);
            wasInAir = false;
        }

        if (isGrounded) jumpsLeft = 1;
    }

    void OnJump(InputAction.CallbackContext ctx){
        if (jumpsLeft > 0){
            velocity.y = jumpForce;
            jumpsLeft--;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public bool IsFalling()
    {
        return velocity.y < -0.1f;
    }
    public void Die(){
        enabled = false;

        GameManager.Instance.GameOver();
    }
}