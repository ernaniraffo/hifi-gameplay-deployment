using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;

    private float speed = 5f;
    private float jumpForce = 3f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Get player rigidbody component
        rb = GetComponent<Rigidbody>();

        // Get the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check if the raycast hits the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.130f);

        // Debug
        Vector3 down = new Vector3(0, -0.130f, 0);
        Debug.Log("isGrounded:" + isGrounded);
        Debug.DrawRay(transform.position, down, Color.black);

        // Check if player wants to jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping");
            Jump();
        }

        // Check if player is still alive
        if (transform.position.y < -20)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the horizontal input (A and D keys or left and right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Get the "right" direction relative to the camera's forward direction
        Vector3 cameraRight = mainCamera.transform.right;

        // Calculate the movement direction based on the camera "right" direction
        Vector3 movement = cameraRight.normalized * horizontalInput * speed * Time.fixedDeltaTime;

        // Apply force to the Rigidbody to move the player
        rb.MovePosition(rb.position + movement);
    }

    void Jump()
    {
        // Add upward force to jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
