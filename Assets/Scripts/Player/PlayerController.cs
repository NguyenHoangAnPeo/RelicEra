using UnityEngine;

public class PlayerController : BaseMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected MonoBehaviour _inputSourceBehaviour;
    [SerializeField] protected CharacterMovement _movement;
    [SerializeField] protected MeleeAttackController _meleeAttack;

    [Header("Jump")]
    [SerializeField, Range(0f, 1f)] protected float _jumpCutMultiplier = 0.5f;
    [SerializeField] protected float _jumpBufferTime = 0.1f;

    protected IMovementInputSource _inputSource;
    protected IAttackInputSource _attackInputSource;

    protected float _jumpBufferCounter;
    protected bool _wasJumpHeld;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadInputSource();
        this.LoadMovement();
        this.LoadMeleeAttack();
    }

    protected override void Awake()
    {
        base.Awake();

        this.CacheInputSource();
    }

    protected virtual void Update()
    {
        if (this._inputSource != null)
        {
            this.HandleJumpInput();
            this.UpdateJumpBuffer();
        }

        this.HandleAttackInput();
    }

    protected virtual void FixedUpdate()
    {
        if (this._inputSource == null || this._movement == null)
            return;

        this.HandleHorizontalMovement();
        this.ProcessJumpBuffer();
        this.HandleJumpCut();
    }

    #region Movement

    protected virtual void HandleHorizontalMovement()
    {
        Vector2 moveDirection = this._inputSource.MoveDirection;

        if (Mathf.Approximately(moveDirection.x, 0f))
            this._movement.StopHorizontal();
        else
            this._movement.Move(moveDirection);
    }

    #endregion

    #region Jump

    protected virtual void HandleJumpInput()
    {
        if (!this._inputSource.JumpPressed)
            return;

        this._jumpBufferCounter = this._jumpBufferTime;

        this._inputSource.ConsumeJump();
    }

    protected virtual void UpdateJumpBuffer()
    {
        if (this._jumpBufferCounter <= 0f)
            return;

        this._jumpBufferCounter -= Time.deltaTime;
    }

    protected virtual void ProcessJumpBuffer()
    {
        if (this._jumpBufferCounter <= 0f)
            return;

        if (!this._movement.CanJump)
            return;

        this._movement.Jump();

        this._jumpBufferCounter = 0f;
    }

    protected virtual void HandleJumpCut()
    {
        if (this._wasJumpHeld && !this._inputSource.JumpHeld)
            this._movement.CutJump(this._jumpCutMultiplier);

        this._wasJumpHeld = this._inputSource.JumpHeld;
    }

    #endregion

    #region Attack

    protected virtual void HandleAttackInput()
    {
        if (this._attackInputSource == null)
            return;

        if (!this._attackInputSource.AttackPressed)
            return;

        if (this._meleeAttack != null)
            this._meleeAttack.Attack();

        this._attackInputSource.ConsumeAttack();
    }

    #endregion

    #region Load Components

    protected virtual void LoadInputSource()
    {
        if (this._inputSourceBehaviour != null)
            return;

        this._inputSourceBehaviour =
            GetComponentInChildren<InputReader>();
    }

    protected virtual void LoadMovement()
    {
        if (this._movement != null)
            return;

        this._movement =
            GetComponentInChildren<CharacterMovement>();
    }

    protected virtual void LoadMeleeAttack()
    {
        if (this._meleeAttack != null)
            return;

        this._meleeAttack =
            GetComponentInChildren<MeleeAttackController>();
    }

    #endregion

    #region Cache

    protected virtual void CacheInputSource()
    {
        this._inputSource =
            this._inputSourceBehaviour as IMovementInputSource;

        this._attackInputSource =
            this._inputSourceBehaviour as IAttackInputSource;
    }

    #endregion
}