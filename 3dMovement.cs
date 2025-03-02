using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing from player object.");
        }
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleInput()
    {
        // Get input from WASD keys or arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the player's forward direction
        moveDirection = transform.right * horizontal + transform.forward * vertical;

        // Check if Shift is held for sprinting
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
    }

    void MovePlayer()
    {
        // Apply movement
        Vector3 movement = moveDirection * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
