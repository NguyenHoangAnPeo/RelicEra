using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : BaseMonoBehaviour
{
    [SerializeField] protected CharacterMovement _movement;

    protected Vector2 _moveInput;
    protected bool _jumpPressed;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
    }

    protected virtual void Update()
    {
        float horizontal = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            horizontal = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            horizontal = 1f;

        _moveInput = new Vector2(horizontal, 0f);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space Pressed");
            _jumpPressed = true;
        }
    }

    protected virtual void FixedUpdate()
    {
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

    protected virtual void LoadMovement()
    {
        if (_movement != null) return;

        _movement = GetComponentInParent<CharacterMovement>();
    }
}