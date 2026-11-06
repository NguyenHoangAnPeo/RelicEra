using UnityEngine;

public abstract class HitBox : BaseMonoBehaviour
{
    [SerializeField] protected float _damage;

    public float Damage => _damage;

    public virtual void BeginAttack(float damage)
    {
        this._damage = damage;
    }

    public virtual void EndAttack()
    {
    }

    protected void DealDamage(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(_damage);
        }
    }
}
