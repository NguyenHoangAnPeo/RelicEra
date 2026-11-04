using UnityEngine;

public abstract class HitBox : BaseMonoBehaviour
{
    [SerializeField] protected float _damage;
    public float Damage => _damage;
    protected void DealDamage(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(_damage);
        }
    }
}
