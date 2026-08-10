using UnityEngine;

public class PlayerController : BaseMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected MonoBehaviour _inputSourceBehaviour;
    [SerializeField] protected CharacterMovement _movement;

    [Header("Jump")]
    [SerializeField, Range(0f, 1f)] protected float _jumpCutMultiplier = 0.5f;
    [SerializeField] protected float _jumpBufferTime = 0.1f;

    protected IMovementInputSource _inputSource;

    protected float _jumpBufferCounter;
    protected bool _wasJumpHeld;

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
        if (this._inputSource == null || this._movement == null) return;

        this.HandleJumpInput();
        this.UpdateJumpBuffer();
    }

    protected virtual void FixedUpdate()
    {
        if (this._inputSource == null || this._movement == null) return;

        this.HandleHorizontalMovement();
        this.ProcessJumpBuffer();
        this.HandleJumpCut();
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
        if (this._jumpBufferCounter <= 0f) return;
        if (!this._movement.CanJump) return;

        this._movement.Jump();

        this._jumpBufferCounter = 0f;
    }

    protected virtual void HandleJumpCut()
    {
        if (this._wasJumpHeld && !this._inputSource.JumpHeld)
            this._movement.CutJump(this._jumpCutMultiplier);

        this._wasJumpHeld = this._inputSource.JumpHeld;
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