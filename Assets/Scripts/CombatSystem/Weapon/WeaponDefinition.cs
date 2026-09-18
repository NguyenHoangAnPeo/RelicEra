using UnityEngine;

public abstract class WeaponDefinition : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] protected string _id;
    [SerializeField] protected string _weaponName;
    [SerializeField] protected Sprite _icon;

    public string Id => _id;
    public string WeaponName => _weaponName;
    public Sprite Icon => _icon;

    [Header("Combat")]
    [SerializeField] protected float _damage = 10f;
    [SerializeField] protected float _attackCooldown = 0.5f;

    public float Damage => _damage;
    public float AttackCooldown => _attackCooldown;

    [Header("Visual")]
    [SerializeField] protected GameObject _weaponPrefab;

    public GameObject WeaponPrefab => _weaponPrefab;

    [Header("Behavior")]
    [SerializeField] protected WeaponBehaviorType _behaviorType;

    public WeaponBehaviorType BehaviorType => _behaviorType;
}

public enum WeaponBehaviorType
{
    Melee,
    Projectile
}