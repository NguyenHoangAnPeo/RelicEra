using UnityEngine;

public class ProjectileHitbox : HitBox
{
    [SerializeField] protected GameObject _weaponGameObj;
    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);

        Destroy(_weaponGameObj);
    }
}