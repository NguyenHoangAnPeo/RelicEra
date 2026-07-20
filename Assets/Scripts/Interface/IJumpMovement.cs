public interface IJumpMovement
{
    bool IsGrounded { get; }
    bool CanJump { get; }

    void Jump();
}