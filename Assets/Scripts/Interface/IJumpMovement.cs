public interface IJumpMovement
{
    bool IsGrounded { get; }
    bool CanJump { get; }

    void Jump();
    void CutJump(float multiplier);
}