using UnityEngine;

public interface IMovementInputSource
{
    Vector2 MoveDirection { get; }
    bool JumpPressed { get; }
    bool JumpHeld { get; }

    void ConsumeJump();
}