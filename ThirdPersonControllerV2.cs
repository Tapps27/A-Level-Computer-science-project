using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonControllerV2 : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        ApplyGravity();
    }

    private void Move()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten forward and right vectors (ignore y-axis)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Combine forward and right vectors based on input
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Move the character in the desired direction
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        // Apply gravity to the character
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
