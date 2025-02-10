using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public Slider HealthBar;

    // Reference to the animator component
    private Animator Animator;
    private float TotalHealth;

    private void Start()
    {
        // Get the Animator component attached to the monster
        Animator = GetComponent<Animator>();

        // Set the starting animation to IdleNormal
        Animator.Play("IdleNormal");

        TotalHealth = Health;
    }

    void Update()
    {
        HealthBar.value = Health / TotalHealth * 100;
    }

    private void FixedUpdate()
    {
        if (DistanceToPlayer() > AttackRange)
        {
            // Move towards the player
            transform.position = MoveTowardPlayer();

            // Face the player
            Vector3 direction = FacePlayer();
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f);
            }

            // Set the "Run" animation to play
            Animator.Play("Run", -1, 0);
        }
        else
        {
            // Stop walking animation when in attack range
            Animator.SetBool("isWalking", false);

            // Trigger attack animation when in attack range
            Animator.SetTrigger("Attack02");

            // Check if enough time has passed since the last attack
            float currentTime = Time.time;
            if (currentTime - LastAttackTime > AttackCooldown)
            {
                Attack();
                LastAttackTime = currentTime;
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Boss attacks the player!");
    }
}
