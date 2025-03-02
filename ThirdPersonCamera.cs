using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The player character
    public float distance = 3.0f; // Default camera distance
    public float minDistance = 2.0f, maxDistance = 6.0f; // Zoom limits
    public float rotationSpeed = 2.0f; // Mouse rotation speed
    public float heightOffset = 1.5f; // Height offset to position camera above player
    public LayerMask collisionLayers; // Layers the camera should collide with

    private float yaw = 0f; // Camera rotation variable

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;
        
        HandleCameraRotation();
        HandleCameraZoom();
        HandleCameraPosition();
    }

    void HandleCameraRotation()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
    }

    void HandleCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(10f, yaw, 0); // Fixed pitch at 10f
        Vector3 targetPosition = target.position + Vector3.up * heightOffset;
        transform.position = targetPosition - (rotation * Vector3.forward * distance);
        transform.LookAt(targetPosition);
    }

    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * 2f;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
}
