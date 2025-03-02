using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update movement in each frame
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        // Set movement parameters based on key inputs
        animator.SetBool("isWalkingForward", Input.GetKey(KeyCode.W));
        animator.SetBool("isWalkingLeft", Input.GetKey(KeyCode.A));
        animator.SetBool("isWalkingRight", Input.GetKey(KeyCode.D));
        animator.SetBool("isWalkingBackwards", Input.GetKey(KeyCode.S));
        animator.SetBool("isRunningForward", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("isRunningLeft", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("isRunningRight", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("isRunningBackwards", Input.GetKey(KeyCode.LeftShift));
    }
}
