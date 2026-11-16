using UnityEngine;

public class AttackController : BaseMonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponController();
    }

    public bool TryAttack()
    {
        if (_weaponController == null)
            LoadWeaponController();

        return _weaponController != null && _weaponController.TryAttack();
    }

    private void LoadWeaponController()
    {
        if (_weaponController != null)
            return;

        _weaponController = GetComponentInChildren<WeaponController>(true);
    }
}
