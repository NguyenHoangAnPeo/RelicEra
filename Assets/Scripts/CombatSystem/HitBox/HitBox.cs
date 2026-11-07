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
        var target = other.GetComponentInParent<IDamageable>()
          ?? other.GetComponentInChildren<IDamageable>();
        if (target != null)
            target.TakeDamage(_damage);
    }
}
