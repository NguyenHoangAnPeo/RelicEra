using System.Collections;
using UnityEngine;

public class SwordHitBox : HitBox
{
    [SerializeField] private Collider2D _collider;
    [SerializeField, Min(0.01f)] private float _attackDuration = 0.15f;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._collider == null)
            this._collider = GetComponentInChildren<Collider2D>();
    }

    protected override void ResetValue()
    {
        base.ResetValue();

        if (this._collider != null)
            this._collider.enabled = false;
    }

    public override void BeginAttack(float damage)
    {
        base.BeginAttack(damage);

        if (this._collider == null)
            return;

        this._collider.enabled = true;
        StartCoroutine(this.EndAttackAfterDelay());
    }

    public override void EndAttack()
    {
        if (this._collider != null)
            this._collider.enabled = false;
    }

    private IEnumerator EndAttackAfterDelay()
    {
        yield return new WaitForSeconds(this._attackDuration);
        this.EndAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
    }
}
