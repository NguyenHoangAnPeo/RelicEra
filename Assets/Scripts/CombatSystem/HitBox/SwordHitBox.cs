using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : HitBox
{
    [SerializeField] private Collider2D _collider;
    [SerializeField, Min(0.01f)] private float _attackDuration = 0.15f;
    private readonly HashSet<IDamageable> _damagedTargets = new HashSet<IDamageable>();
    // Keep one timer so an earlier attack cannot disable the collider during a newer attack.
    private Coroutine _endAttackCoroutine;

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
        this._damagedTargets.Clear();

        // Restart the timer whenever a new attack begins.
        if (this._endAttackCoroutine != null)
        {
            StopCoroutine(this._endAttackCoroutine);
            this._endAttackCoroutine = null;
        }

        if (this._collider == null)
            return;

        this._collider.enabled = true;
        this._endAttackCoroutine = StartCoroutine(this.EndAttackAfterDelay());
    }

    public override void EndAttack()
    {
        if (this._endAttackCoroutine != null)
        {
            StopCoroutine(this._endAttackCoroutine);
            this._endAttackCoroutine = null;
        }

        if (this._collider != null)
            this._collider.enabled = false;
    }

    private IEnumerator EndAttackAfterDelay()
    {
        yield return new WaitForSeconds(this._attackDuration);

        if (this._collider != null)
            this._collider.enabled = false;

        this._endAttackCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponentInParent<IDamageable>()
            ?? other.GetComponentInChildren<IDamageable>();

        if (target == null || !this._damagedTargets.Add(target))
            return;

        target.TakeDamage(_damage);
    }
}
