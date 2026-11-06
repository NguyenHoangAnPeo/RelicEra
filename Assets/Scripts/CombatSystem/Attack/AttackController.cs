using UnityEngine;

public class AttackController : BaseMonoBehaviour
{
    [SerializeField] private WeaponDefinitionSO _currentWeapon;
    [SerializeField] protected HitBox _weaponHitbox;
    public HitBox WeaponHitbox => _weaponHitbox;

    private IWeaponAttackBehavior _attackBehavior;
    private float _nextAttackTime;

    protected override void Awake()
    {
        base.Awake();
        SetWeapon(_currentWeapon);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHitBoxWP();
    }
    public void SetWeapon(WeaponDefinitionSO weapon)
    {
        _currentWeapon = weapon;
        _attackBehavior = weapon == null ? null : weapon.CreateAttackBehavior();
    }

    public bool TryAttack()
    {
        if (this._weaponHitbox == null)
            this.LoadHitBoxWP();

        if (_currentWeapon == null || _attackBehavior == null || this._weaponHitbox == null)
        {
            return false;
        }

        if (Time.time < _nextAttackTime)
        {
            return false;
        }

        _attackBehavior.Attack(_currentWeapon);
        this._weaponHitbox.BeginAttack(_currentWeapon.Damage);
        _nextAttackTime = Time.time + _currentWeapon.AttackCooldown;

        return true;
    }

    #region Load Component
    protected virtual void LoadHitBoxWP()
    {
        if (this._weaponHitbox != null) return;
        this._weaponHitbox = transform.GetComponentInChildren<HitBox>();
    }
    #endregion
}
