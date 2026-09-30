using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private WeaponDefinition _currentWeapon;

    private IWeaponAttackBehavior _attackBehavior;
    private float _nextAttackTime;

    private void Awake()
    {
        SetWeapon(_currentWeapon);
    }

    public void SetWeapon(WeaponDefinition weapon)
    {
        _currentWeapon = weapon;
        _attackBehavior = weapon.CreateAttackBehavior();
    }

    public bool TryAttack()
    {
        if (_currentWeapon == null)
        {
            return false;
        }

        if (Time.time < _nextAttackTime)
        {
            return false;
        }

        _attackBehavior.Attack(_currentWeapon);
        _nextAttackTime = Time.time + _currentWeapon.AttackCooldown;

        return true;
    }
}