using UnityEngine;

public abstract class Mob : Entity, IDamageable
{
    public float Health;
    public float JumpForce;
    public float MoveSpeed;

    public virtual void Damage(float damage)
    {
        Health -= damage;
    }

    protected void Jump()
    {
        Rigidbody.velocity = new(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

        Rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
}
