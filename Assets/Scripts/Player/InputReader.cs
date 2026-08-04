using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : BaseMonoBehaviour,IMovementInputSource
{
    [SerializeField] protected CharacterMovement _movement;

    protected PlayerInputActions _inputActions;

    protected Vector2 _moveDirection;
    protected bool _jumpPressed;
    protected bool _jumpHeld;
    public Vector2 MoveDirection => _moveDirection;
    public bool JumpPressed => _jumpPressed;
    public bool JumpHeld => _jumpHeld;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
    }
    protected override void Awake()
    {
        base.Awake();

        _inputActions = new PlayerInputActions();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        _inputActions.Player.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        _inputActions.Player.Jump.performed += OnJumpPerformed;
        _inputActions.Player.Jump.canceled += OnJumpCanceled;
    }

    protected override void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;

        _inputActions.Player.Jump.performed -= OnJumpPerformed;
        _inputActions.Player.Jump.canceled -= OnJumpCanceled;

        _inputActions.Player.Disable();

        base.OnDisable();
    }

    protected virtual void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveDirection = Vector2.zero;
    }

    protected virtual void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _jumpPressed = true;
        _jumpHeld = true;
    }

    protected virtual void OnJumpCanceled(InputAction.CallbackContext context)
    {
        _jumpHeld = false;
    }

    public virtual void ConsumeJump()
    {
        _jumpPressed = false;
    }
    protected virtual void LoadMovement()
    {
        if (_movement != null) return;

        _movement = GetComponentInParent<CharacterMovement>();
    }
}
