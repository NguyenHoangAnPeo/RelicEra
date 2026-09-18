using UnityEngine;

[CreateAssetMenu(
    fileName = "MeleeWeaponDefinition",
    menuName = "Relic Era/Weapons/Melee Weapon"
)]
public class MeleeWeaponDefinition : WeaponDefinition
{
    [Header("Melee")]
    [SerializeField] protected Vector2 _hitboxSize = new Vector2(1f, 1f);
    [SerializeField] protected Vector2 _hitboxOffset = new Vector2(0.5f, 0f);

    public Vector2 HitboxSize => _hitboxSize;
    public Vector2 HitboxOffset => _hitboxOffset;


    [Header("Knockback")]
    [SerializeField] protected float _knockbackForce = 0f;

    public float KnockbackForce => _knockbackForce;


    [Header("Combo")]
    [SerializeField] protected bool _canCombo;
    [SerializeField] protected int _maxCombo = 1;

    public bool CanCombo => _canCombo;
    public int MaxCombo => _maxCombo;
}