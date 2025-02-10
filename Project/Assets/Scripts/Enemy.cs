using UnityEngine;

public abstract class Enemy : Mob
{
    public float AttackRange;
    public float AttackCooldown;
    public int AttackDamage;
    public float MaxVisionDistance;

    protected float LastAttackTime;

    protected bool CanSeePlayer()
    {
        return Physics.Raycast(transform.position, FacePlayer(), MaxVisionDistance);
    }

    protected Vector3 MoveTowardPlayer()
    {
        return Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed);
    }

    protected Vector3 FacePlayer()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        direction.y = 0;
        return direction;
    }

    protected float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    protected virtual void Attack()
    {
        PlayerScript.Damage(AttackDamage);
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (Health <= 0) Destroy(gameObject);
    }
}
