using UnityEngine;

public class Weapon : BaseMonoBehaviour
{
    [SerializeField] protected WeaponDefinitionSO _weaponData;
    public WeaponDefinitionSO WeaponData => _weaponData;


}
