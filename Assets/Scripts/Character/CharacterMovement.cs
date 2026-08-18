using UnityEngine;

public class CharacterMovement : BaseMonoBehaviour, IMovementMotor, IJumpMovement
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    [SerializeField] protected Transform _character;
    public Transform Character => _character;
    [SerializeField] protected Collider2D _collider2D;
    public Collider2D Collider2D => _collider2D;

    [Header("Movement")]
    [SerializeField] protected float _moveSpeed = 6f;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] protected float _jumpForce = 12f;
    public float JumpForce => _jumpForce;

    [SerializeField] protected float _coyoteTime = 0.1f;

    protected float _coyoteCounter;

    [Header("Ground Check")]
    [SerializeField] protected Transform _groundCheckPoint;
    [SerializeField] protected Vector2 _groundCheckSize = new Vector2(0.8f, 0.12f);
    [SerializeField] protected LayerMask _groundLayer;

    protected bool _isFacingRight = true;
    public Vector2 Velocity => this._rigidbody2D == null ? Vector2.zero : this._rigidbody2D.linearVelocity;
    public bool IsFacingRight => this._isFacingRight;

    protected bool _isGrounded;
    public bool IsGrounded => this._isGrounded;
    public bool CanJump => this._coyoteCounter > 0f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
        this.LoadTransformChar();
        this.LoadColider();
    }

    protected virtual void FixedUpdate()
    {
        this.UpdateGrounded();
    }
    public virtual void Move(Vector2 direction)
    {
        if (_rigidbody2D == null) return;

        float horizontalDirection = 0f;

        if (direction.x > 0f)
        {
            horizontalDirection = 1f;
        }
        else if (direction.x < 0f)
        {
            horizontalDirection = -1f;
        }

        float horizontalVelocity = horizontalDirection * _moveSpeed;

        _rigidbody2D.linearVelocity = new Vector2(
            horizontalVelocity,
            _rigidbody2D.linearVelocity.y
        );

        FaceMoveDirection(horizontalDirection);
    }
    public virtual void Jump()
    {
        if (!this.CanJump || this._rigidbody2D == null) return;

        this._coyoteCounter = 0f;
        this._rigidbody2D.linearVelocity = new Vector2(this._rigidbody2D.linearVelocity.x, this._jumpForce);
    }
    protected virtual void UpdateGrounded()
    {
        Vector2 checkPosition = this._groundCheckPoint == null
            ? this.transform.position
            : this._groundCheckPoint.position;

        this._isGrounded = Physics2D.OverlapBox(
            checkPosition,
            this._groundCheckSize,
            0f,
            this._groundLayer
        ) != null;

        if (this._isGrounded)
        {
            this._coyoteCounter = this._coyoteTime;
        }
        else
        {
            this._coyoteCounter -= Time.fixedDeltaTime;
        }
    }
    protected virtual void FaceMoveDirection(float horizontalDirection)
    {
        if (this._character == null) return;
        if (Mathf.Approximately(horizontalDirection, 0f)) return;

        bool shouldFaceRight = horizontalDirection > 0f;
        if (shouldFaceRight == this._isFacingRight) return;

        this._isFacingRight = shouldFaceRight;
        Vector3 localScale = this._character.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (this._isFacingRight ? 1f : -1f);
        this._character.localScale = localScale;
    }
    public virtual void StopHorizontal()
    {
        if (this._rigidbody2D == null) return;
        this._rigidbody2D.linearVelocity = new Vector2(0f, this._rigidbody2D.linearVelocity.y);
    }
    public virtual void CutJump(float multiplier)
    {
        if (this._rigidbody2D == null) return;
        if (this._rigidbody2D.linearVelocity.y <= 0f) return;

        float clampedMultiplier = Mathf.Clamp01(multiplier);
        this._rigidbody2D.linearVelocity = new Vector2(
            this._rigidbody2D.linearVelocity.x,
            this._rigidbody2D.linearVelocity.y * clampedMultiplier
        );
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
    protected virtual void OnDrawGizmosSelected()
    {
        Vector2 checkPosition = this._groundCheckPoint == null ? this.transform.position : this._groundCheckPoint.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(checkPosition, this._groundCheckSize);
    }
}
