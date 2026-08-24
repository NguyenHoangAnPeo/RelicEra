using UnityEngine;

public class MeleeAttackController : AttackController
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector2 _attackSize = Vector2.one;
    [SerializeField] private LayerMask _targetLayer;

    public override void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            this._attackPoint.position,
            this._attackSize,
            0f,
            this._targetLayer
        );

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable =
                hit.GetComponent<IDamageable>();

            if (damageable == null)
                continue;

            if (damageable.IsDead)
                continue;

            damageable.TakeDamage(this._damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (this._attackPoint == null)
            return;

        Gizmos.DrawWireCube(
            this._attackPoint.position,
            this._attackSize
        );
    }
}