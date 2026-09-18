using UnityEngine;

[CreateAssetMenu(
    fileName = "ProjectileWeaponDefinition",
    menuName = "Relic Era/Weapons/Projectile Weapon"
)]
public class ProjectileWeaponDefinition : WeaponDefinition
{
    [Header("Projectile")]
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected float _projectileSpeed = 10f;

    public GameObject ProjectilePrefab => _projectilePrefab;
    public float ProjectileSpeed => _projectileSpeed;


    [Header("Ammo")]
    [SerializeField] protected int _ammoCount = 1;

    public int AmmoCount => _ammoCount;


    [Header("Spread")]
    [SerializeField] protected float _spreadAngle = 0f;

    public float SpreadAngle => _spreadAngle;
}