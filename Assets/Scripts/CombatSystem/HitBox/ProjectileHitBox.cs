using UnityEngine;

public class ProjectileHitbox : HitBox
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IDamageable>(out _))
            return;

        DealDamage(other);

        Destroy(gameObject);
    }
}