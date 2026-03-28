public interface IMovementBlocker
{
    bool IsMovementBlocked { get; }
    void ToggleMovementBlock();
}
