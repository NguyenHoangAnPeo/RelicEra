public class SwordAttackBehavior : IWeaponAttackBehavior
{
    public bool Attack(WeaponController weaponController)
    {
        if (weaponController == null || weaponController.WeaponData == null)
            return false;

        var swordHitBox = weaponController.WeaponHitbox as SwordHitBox;
        if (swordHitBox == null)
            return false;

        swordHitBox.BeginAttack(weaponController.WeaponData.Damage);
        return true;
    }
}
