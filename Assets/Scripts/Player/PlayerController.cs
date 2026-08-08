using UnityEngine;

public class PlayerController : BaseMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected MonoBehaviour _inputSourceBehaviour;
    [SerializeField] protected CharacterMovement _movement;

    protected IMovementInputSource _inputSource;

    [Header("Setting Movement")]
    [SerializeField] protected float _jumpBufferTime = 0.1f;

    protected float _jumpBufferCounter;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadInputSource();
        this.LoadMovement();
    }

    protected override void Awake()
    {
        base.Awake();

        this.CacheInputSource();
    }

    protected virtual void Update()
    {
        this.HandleMovementInput();
        this.UpdateJumpBuffer();
    }

    protected virtual void FixedUpdate()
    {
        this.ProcessJumpBuffer();
    }

    protected virtual void HandleMovementInput()
    {
        if (this._inputSource == null || this._movement == null) return;

        this.HandleHorizontalMovement();
        this.HandleJumpInput();
    }

    protected virtual void HandleHorizontalMovement()
    {
        Vector2 moveDirection = this._inputSource.MoveDirection;

        if (Mathf.Approximately(moveDirection.x, 0f))
            this._movement.StopHorizontal();
        else
            this._movement.Move(moveDirection);
    }

    protected virtual void HandleJumpInput()
    {
        if (!this._inputSource.JumpPressed) return;

        this._jumpBufferCounter = this._jumpBufferTime;

        this._inputSource.ConsumeJump();
    }

    protected virtual void UpdateJumpBuffer()
    {
        if (this._jumpBufferCounter <= 0f) return;

        this._jumpBufferCounter -= Time.deltaTime;
    }

    protected virtual void ProcessJumpBuffer()
    {
        if (_jumpBufferCounter <= 0f) return;
        if (!_movement.CanJump) return;

        _movement.Jump();
        _jumpBufferCounter = 0f;
    }

    protected virtual void LoadInputSource()
    {
        if (this._inputSourceBehaviour != null) return;

        this._inputSourceBehaviour =
            GetComponentInChildren<InputReader>();
    }

    protected virtual void LoadMovement()
    {
        if (this._movement != null) return;

        this._movement =
            GetComponentInChildren<CharacterMovement>();
    }

    protected virtual void CacheInputSource()
    {
        this._inputSource =
            this._inputSourceBehaviour as IMovementInputSource;
    }
}