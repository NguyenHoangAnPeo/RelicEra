using UnityEngine;

public class SwordHitBox : HitBox
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
    }
}
