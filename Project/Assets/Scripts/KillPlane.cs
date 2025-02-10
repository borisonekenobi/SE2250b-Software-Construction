using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<IDamageable>(out var damageable)) damageable.Damage(999);
        else if (other.transform.parent.TryGetComponent<IDamageable>(out var damageableParent)) damageableParent.Damage(999);
        else Destroy(other);
    }
}
