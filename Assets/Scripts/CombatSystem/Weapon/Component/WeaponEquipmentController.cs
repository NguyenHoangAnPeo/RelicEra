using UnityEngine;

public class WeaponEquipmentController : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;

    [SerializeField] private WeaponController _currentWeapon;
    public WeaponController CurrentWeapon => _currentWeapon;

    public void Equip(WeaponController weapon)
    {
        if (weapon == null || weaponHolder == null)
            return;

        Unequip();

        _currentWeapon = weapon;
        weapon.transform.SetParent(weaponHolder, false);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.gameObject.SetActive(true);
    }

    public void Unequip()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = null;
    }
}
