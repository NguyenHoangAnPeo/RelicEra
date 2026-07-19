using UnityEngine;

public class CharacterMovement : BaseMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    [SerializeField] protected Transform _character;
    public Transform Character => _character;
    [SerializeField] protected Collider2D _collider2D;
    public Collider2D Collider2D => _collider2D;
    [Header("Setting")]
    [SerializeField] protected float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] protected float _jumpForce;
    public float JumpForce => _jumpForce;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
        this.LoadTransformChar();
        this.LoadColider();
    }
    protected virtual void LoadRigibody()
    {
        if (this._rigidbody2D != null) return;
        this._rigidbody2D = transform.GetComponentInParent<Rigidbody2D>();
    }
    protected virtual void LoadTransformChar()
    {
        if (this._character != null) return;
        this._character = transform.GetComponentInParent<Transform>();
    }
    protected virtual void LoadColider()
    {
        if (this._collider2D != null) return;
        this._collider2D = transform.GetComponentInParent<Collider2D>();
    }
}
