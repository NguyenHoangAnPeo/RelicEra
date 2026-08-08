using UnityEngine;

public interface IMovementMotor
{
    Vector2 Velocity { get; }
    bool IsFacingRight { get; }

    void Move(Vector2 direction);
    void StopHorizontal();
}