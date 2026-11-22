using UnityEngine;

public class AttackController : BaseMonoBehaviour
{
    [SerializeField] private WeaponEquipmentController _weaponEquipmentController;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponEquipmentController();
    }

    public bool TryAttack()
    {
        WeaponController weaponController = _weaponEquipmentController != null
            ? _weaponEquipmentController.CurrentWeapon
            : null;

        return weaponController != null && weaponController.TryAttack();
    }

    private void LoadWeaponEquipmentController()
    {
        if (_weaponEquipmentController != null)
            return;

        _weaponEquipmentController = GetComponentInParent<WeaponEquipmentController>();
        if (_weaponEquipmentController != null)
            return;

        _weaponEquipmentController = transform.root.GetComponentInChildren<WeaponEquipmentController>(true);
    }
}
