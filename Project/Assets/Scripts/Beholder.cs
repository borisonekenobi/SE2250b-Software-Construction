using UnityEngine;

public class Beholder : Enemy
{
    private Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (DistanceToPlayer() > AttackRange)
        {
            Vector3 moveDirection = MoveTowardPlayer();
            moveDirection.y = transform.position.y;
            transform.position = moveDirection;

            Vector3 direction = FacePlayer();
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f);
            }
            return;
        }

        float currentTime = Time.time;
        if (currentTime - LastAttackTime > AttackCooldown)
        {
            Attack();
            LastAttackTime = currentTime;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Beholder Monster attacks the player!");
    }
}
