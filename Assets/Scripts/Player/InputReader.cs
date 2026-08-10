using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : BaseMonoBehaviour, IMovementInputSource
{
    [Header("Input Actions")]
    [SerializeField] protected InputActionReference _moveAction;
    [SerializeField] protected InputActionReference _jumpAction;

    protected Vector2 _moveDirection;
    protected bool _jumpPressed;
    protected bool _jumpHeld;

    public Vector2 MoveDirection => _moveDirection;
    public bool JumpPressed => _jumpPressed;
    public bool JumpHeld => _jumpHeld;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.EnableInputActions();
    }

    protected override void OnDisable()
    {
        this.DisableInputActions();
        base.OnDisable();
    }

    protected virtual void EnableInputActions()
    {
        InputAction moveAction = this._moveAction == null ? null : this._moveAction.action;
        if (moveAction != null)
        {
            moveAction.performed += this.OnMovePerformed;
            moveAction.canceled += this.OnMoveCanceled;
            moveAction.Enable();
        }

        InputAction jumpAction = this._jumpAction == null ? null : this._jumpAction.action;
        if (jumpAction != null)
        {
            jumpAction.performed += this.OnJumpPerformed;
            jumpAction.canceled += this.OnJumpCanceled;
            jumpAction.Enable();
        }
    }

    protected virtual void DisableInputActions()
    {
        InputAction moveAction = this._moveAction == null ? null : this._moveAction.action;
        if (moveAction != null)
        {
            moveAction.performed -= this.OnMovePerformed;
            moveAction.canceled -= this.OnMoveCanceled;
            moveAction.Disable();
        }

        InputAction jumpAction = this._jumpAction == null ? null : this._jumpAction.action;
        if (jumpAction != null)
        {
            jumpAction.performed -= this.OnJumpPerformed;
            jumpAction.canceled -= this.OnJumpCanceled;
            jumpAction.Disable();
        }

        this._moveDirection = Vector2.zero;
        this._jumpPressed = false;
        this._jumpHeld = false;
    }

    protected virtual void OnMovePerformed(InputAction.CallbackContext context)
    {
        this._moveDirection = context.ReadValue<Vector2>();
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        this._moveDirection = Vector2.zero;
    }

    protected virtual void OnJumpPerformed(InputAction.CallbackContext context)
    {
        this._jumpPressed = true;
        this._jumpHeld = true;
    }

    protected virtual void OnJumpCanceled(InputAction.CallbackContext context)
    {
        this._jumpHeld = false;
    }

    public virtual void ConsumeJump()
    {
        this._jumpPressed = false;
    }
}