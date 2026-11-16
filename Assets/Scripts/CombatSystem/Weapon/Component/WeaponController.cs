using UnityEngine;

public class WeaponController : BaseMonoBehaviour
{
    [SerializeField] private WeaponDefinitionSO _weaponData;
    [SerializeField] private HitBox _weaponHitbox;

    public WeaponDefinitionSO WeaponData => _weaponData;

    private IWeaponAttackBehavior _attackBehavior;
    private float _nextAttackTime;

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

        if (_weaponData == null || _attackBehavior == null || _weaponHitbox == null)
            return false;

        if (Time.time < _nextAttackTime)
            return false;

        _attackBehavior.Attack(_weaponData);
        _weaponHitbox.BeginAttack(_weaponData.Damage);
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
