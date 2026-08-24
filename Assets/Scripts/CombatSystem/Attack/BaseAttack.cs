using UnityEngine;

public abstract class AttackController : BaseMonoBehaviour
{
    [SerializeField] protected float _damage = 10f;
    public abstract void Attack();
}