using UnityEngine;

public interface IMovementInputSource
{
    Vector2 MoveDirection { get; }
    bool JumpPressed { get; }
}