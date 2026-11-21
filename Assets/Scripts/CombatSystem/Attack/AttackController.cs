using UnityEngine;

public class AttackController : BaseMonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;

    public void SetWeaponController(WeaponController weaponController)
    {
        _weaponController = weaponController;
    }

    public bool TryAttack()
    {
        return _weaponController != null && _weaponController.TryAttack();
    }
}
