using UnityEngine;

public class MovementBlockingInputService :  IInputService, IMovementBlocker
{
    private readonly IInputService _inputService;

    public MovementBlockingInputService(IInputService inputService)
    {
        _inputService = inputService;
    }

    public bool IsMovementBlocked { get; private set; }

    public void ToggleMovementBlock()
    {
        IsMovementBlocked = !IsMovementBlocked;
    }

    public Vector2 GetMoveDirection()
    {
        if (IsMovementBlocked)
            return Vector2.zero;

        return _inputService.GetMoveDirection();
    }
}
