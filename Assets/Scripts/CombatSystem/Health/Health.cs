using UnityEngine;

public class Health : BaseMonoBehaviour,IDamageable
{
    [SerializeField] protected float _maxHealth = 100f;
    public float MaxHealth => _maxHealth;
    [SerializeField] protected float _currentHealth;
    public float CurrentHealth => _currentHealth;
    protected bool _isDead => _currentHealth <= 0;
    public bool IsDead => _isDead;
    protected override void Awake()
    {
        base.Awake();
        this._currentHealth = this._maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (_isDead) return;
        this._currentHealth -= damage;

        if (this._currentHealth <= 0)
        {
            this.Die();
        }
    }
    private void Die()
    {
        Debug.Log("Character died");
    }
}
