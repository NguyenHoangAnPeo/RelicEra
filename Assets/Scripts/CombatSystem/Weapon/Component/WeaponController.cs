using UnityEngine;

public class WeaponController : BaseMonoBehaviour
{
    [SerializeField] protected WeaponDefinitionSO _weaponData;
    [SerializeField] protected HitBox _weaponHitbox;

    public WeaponDefinitionSO WeaponData => _weaponData;
    public HitBox WeaponHitbox => _weaponHitbox;

    protected IWeaponAttackBehavior _attackBehavior;
    protected float _nextAttackTime;

    protected override void Awake()
    {
        base.Awake();
        SetWeapon(_weaponData);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHitBox();
    }

    public void SetWeapon(WeaponDefinitionSO weapon)
    {
        _weaponData = weapon;
        _attackBehavior = weapon == null ? null : weapon.CreateAttackBehavior();
        _nextAttackTime = 0f;
    }

    public bool TryAttack()
    {
        if (_weaponHitbox == null)
            LoadHitBox();

        if (_weaponData == null || _attackBehavior == null)
            return false;

        if (Time.time < _nextAttackTime)
            return false;

        if (!_attackBehavior.Attack(this))
            return false;

        _nextAttackTime = Time.time + _weaponData.AttackCooldown;

        return true;
    }

    private void LoadHitBox()
    {
        if (_weaponHitbox != null)
            return;

        _weaponHitbox = GetComponentInChildren<HitBox>(true);
    }
}
