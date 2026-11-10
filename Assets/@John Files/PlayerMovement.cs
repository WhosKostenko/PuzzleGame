using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
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

        // Consume the press even in midair.
        jumpRequested = false;
        verticalVelocity += gravity * Time.deltaTime;

        Vector2 input = Vector2.ClampMagnitude(moveInput, 1f);
        Vector3 horizontal = transform.TransformDirection(new Vector3(input.x, 0f, input.y));
        Vector3 velocity = horizontal * playerSpeed + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);

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
