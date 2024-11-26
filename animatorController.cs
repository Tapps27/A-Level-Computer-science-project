using UnityEngine;

public class CharacterComboAttack : MonoBehaviour
{
    private Animator animator;
    private int comboStep = 0;
    private float comboTimer = 0f;
    private float maxComboDelay = 1.0f; // Time allowed between combo steps in seconds

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update movement and combo in each frame
        HandleMovementInput();
        HandleAttackCombo();
    }

    private void HandleMovementInput()
    {
        // Set movement parameters based on key inputs
        animator.SetBool("isWalkingForward", Input.GetKey(KeyCode.W));
        animator.SetBool("isWalkingLeft", Input.GetKey(KeyCode.A));
        animator.SetBool("isWalkingRight", Input.GetKey(KeyCode.D));
        animator.SetBool("isWalkingBackwards", Input.GetKey(KeyCode.S));
    }

    private void HandleAttackCombo()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Only increment combo if we’re still within the combo sequence time limit
            if (comboTimer > 0 && comboStep < 3)
            {
                comboStep++;
            }
            else
            {
                // Start new combo if combo timer expired
                comboStep = 1;
            }

            // Trigger the appropriate attack animation based on the combo step
            PlayAttackAnimation();

            // Reset combo timer
            comboTimer = maxComboDelay;
        }

        // Reduce combo timer over time
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            // Reset combo if time runs out
            comboStep = 0;
        }
    }

    private void PlayAttackAnimation()
    {
        // Reset all triggers to prevent any overlapping animations
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");

        // Trigger the specific attack based on the combo step
        switch (comboStep)
        {
            case 1:
                animator.SetTrigger("attack1");
                break;
            case 2:
                animator.SetTrigger("attack2");
                break;
            case 3:
                animator.SetTrigger("attack3");
                break;
        }
    }
}
