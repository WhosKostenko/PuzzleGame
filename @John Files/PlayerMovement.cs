using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float currentSpeed;
    public float playerSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpHeight = 1.5f;
    float gravity = -9.81f;

    CharacterController controller;
    Vector2 moveInput;
    float verticalVelocity;
    bool jumpRequested;

    public Animator anim;
    bool isMoving;
    bool isJumping;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;
        }
        else if (controller.isGrounded)
        {
            verticalVelocity = -2f;
        }

        if (jumpRequested && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //movement check
    
        if(Keyboard.current.leftShiftKey.isPressed) {
            currentSpeed = runSpeed;
            anim.SetBool("isRunning",true);
            Debug.Log("Running");
        }else {
            currentSpeed = playerSpeed;
            anim.SetBool("isRunning",false);
        }
        

        // Consume the press even in midair.
        jumpRequested = false;
        verticalVelocity += gravity * Time.deltaTime;

        Vector2 input = Vector2.ClampMagnitude(moveInput, 1f);
        Vector3 horizontal = transform.TransformDirection(new Vector3(input.x, 0f, input.y));
        Vector3 velocity = horizontal * currentSpeed + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);

        anim.SetFloat("MoveX",moveInput.x);
        anim.SetFloat("MoveY",moveInput.y);

        isMoving = moveInput.magnitude > 0.1f;
        isJumping = !controller.isGrounded;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpRequested = true;
        }
    }

    void OnDisable()
    {
        moveInput = Vector2.zero;
        jumpRequested = false;
        verticalVelocity = 0f;
    }

    /*private FixedUpdate(){
        rb.velocity = moveDirection * moveSpeed;

        if(rb.velocity.x > 0 || rb.velocity.z >0) {
            anim.SetBool("isMoving", true);
        }else(

        )
    }
    */
}
