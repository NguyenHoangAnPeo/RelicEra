using UnityEngine;

public class PlayerController : BaseMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected MonoBehaviour _inputSourceBehaviour;
    [SerializeField] protected CharacterMovement _movement;

    protected IMovementInputSource _inputSource;

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

    protected virtual void FixedUpdate()
    {
        this.HandleMovementInput();
    }

    protected virtual void HandleMovementInput()
    {
        if (this._inputSource == null || this._movement == null) return;

        Vector2 moveDirection = this._inputSource.MoveDirection;
        if (Mathf.Approximately(moveDirection.x, 0f))
            this._movement.StopHorizontal();
        else
            this._movement.Move(moveDirection);

        if (!this._inputSource.JumpPressed) return;

        this._movement.Jump();
        this._inputSource.ConsumeJump();
    }
    protected virtual void LoadInputSource()
    {
        if (this._inputSourceBehaviour != null) return;
        this._inputSourceBehaviour = GetComponentInChildren<InputReader>();
    }

    protected virtual void LoadMovement()
    {
        if (this._movement != null) return;
        this._movement = GetComponentInChildren<CharacterMovement>();
    }

    protected virtual void CacheInputSource()
    {
        this._inputSource = this._inputSourceBehaviour as IMovementInputSource;
    }
}