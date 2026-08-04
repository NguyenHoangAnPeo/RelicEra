using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : BaseMonoBehaviour
{
    [SerializeField] protected CharacterMovement _movement;

    protected PlayerInputActions _inputActions;
    protected Vector2 _moveInput;
    protected bool _jumpPressed;

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
        _inputActions.Player.Jump.performed += OnJumpPerformed;
    }
    protected override void OnDisable()
    {
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
        _inputActions.Player.Disable();

        base.OnDisable();
    }
    protected virtual void Update()
    {
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    protected virtual void FixedUpdate()
    {
        if (_movement == null) return;

        if (Mathf.Approximately(_moveInput.x, 0f))
            _movement.StopHorizontal();
        else
            _movement.Move(_moveInput);

        if (_jumpPressed)
        {
            _movement.Jump();
            _jumpPressed = false;
        }
    }
    protected virtual void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _jumpPressed = true;
    }
    protected virtual void LoadMovement()
    {
        if (_movement != null) return;

        _movement = GetComponentInParent<CharacterMovement>();
    }
}