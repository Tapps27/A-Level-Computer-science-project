using UnityEngine;
using System.Collections;

public class AI_Enemy : MonoBehaviour {
    public GameObject Target;
    private Animator myAnimator;
    private UnityEngine.AI.NavMeshAgent agent;
    public float attackRange = 3.0f; // Increased attack range for testing
    public float detectionRange = 10.0f;
    public float stoppingDistance = 1.5f; // AI stops at this distance to avoid pushing the player
    public float attackCooldown = 2.0f;
    private float lastAttackTime;

    void Start() {
        myAnimator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        if (agent == null) {
            Debug.LogError("NavMeshAgent not found on " + gameObject.name);
        }
        agent.stoppingDistance = stoppingDistance; // Ensure AI stops at a proper distance

        // Force an attack to see if animations work
        StartCoroutine(TestImmediateAttack());
    }

    void Update() {
        if (Target != null) {
            float distance = Vector3.Distance(transform.position, Target.transform.position);
            Debug.Log("Distance to target: " + distance);
            
            if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown) {
                Debug.Log("Enemy is in attack range!");
                Attack();
            } else if (distance <= detectionRange && distance > stoppingDistance) {
                ChaseTarget();
            } else {
                Idle();
            }
        }
    }

    void ClearAllBool() {
        myAnimator.SetBool("defy", false);
        myAnimator.SetBool("idle", false);
        myAnimator.SetBool("dizzy", false);
        myAnimator.SetBool("walk", false);
        myAnimator.SetBool("run", false);
        myAnimator.SetBool("jump", false);
        myAnimator.SetBool("die", false);
        myAnimator.SetBool("jump_left", false);
        myAnimator.SetBool("jump_right", false);
        myAnimator.SetBool("attack_01", false);
        myAnimator.SetBool("attack_02", false);
        myAnimator.SetBool("attack_03", false);
        myAnimator.SetBool("damage", false);
    }

    void ChaseTarget() {
        ClearAllBool();
        myAnimator.SetBool("run", true);
        agent.isStopped = false;
        agent.SetDestination(Target.transform.position);
    }

    void Attack() {
        Debug.Log("Attack function triggered!"); // Debug message
        ClearAllBool();
        agent.isStopped = true;
        agent.velocity = Vector3.zero; // Ensure AI fully stops

        int attackType = Random.Range(1, 4);
        string attackAnimation = "attack_0" + attackType;

        Debug.Log("AI Attacking with animation: " + attackAnimation);

        myAnimator.SetBool(attackAnimation, true);
        lastAttackTime = Time.time;
        
        StartCoroutine(ResetAttack(attackAnimation));
    }

    IEnumerator ResetAttack(string attackAnimation) {
        yield return new WaitForSeconds(1.2f); // Ensure this matches attack animation length

        Debug.Log("Resetting attack: " + attackAnimation);
        
        myAnimator.SetBool(attackAnimation, false);
        agent.isStopped = false;
        myAnimator.SetBool("idle", true); // Ensure AI returns to idle after attacking
    }

    IEnumerator TestImmediateAttack() {
        yield return new WaitForSeconds(2.0f); // Give time for animations to initialize
        Attack();
    }

    void Idle() {
        ClearAllBool();
        myAnimator.SetBool("idle", true);
    }

    public void TakeDamage() {
        ClearAllBool();
        myAnimator.SetBool("damage", true);
    }

    public void Die() {
        ClearAllBool();
        myAnimator.SetBool("die", true);
        agent.isStopped = true;
        this.enabled = false;
    }
}