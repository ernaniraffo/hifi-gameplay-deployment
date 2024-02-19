using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0.5f;
    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from A and D keys
        float horizontalInput = Input.GetAxis("Horizontal");

        // Get the camera's right vector
        Transform cameraTransform = Camera.main.transform;
        Vector3 right = cameraTransform.right;

        // Project movement input onto the camera's right vector
        Vector3 movement = right * horizontalInput;

        // Move the player using the CharacterController
        MovePlayer(movement);

        // Handle jumping
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void MovePlayer(Vector3 movement)
    {
        // Convert movement to world space
        Vector3 moveDirection = transform.TransformDirection(movement);

        // Apply movement to the CharacterController
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Apply gravity
        ApplyGravity();
    }

    void Jump()
    {
        // Apply the jump force if the player is grounded
        if (characterController.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    void ApplyGravity()
    {
        // Apply gravity to the player
        if (!characterController.isGrounded)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            // If grounded, reset the vertical velocity
            velocity.y = -0.5f;
        }

        // Move the player vertically
        characterController.Move(velocity * Time.deltaTime);
    }
}
