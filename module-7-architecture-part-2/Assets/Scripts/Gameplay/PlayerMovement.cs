using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IMovementService _movementService;

    public void Initialize(IMovementService movementService)
    {
        _movementService = movementService;
    }

    public void Move(Vector2 direction)
    {
        _movementService.Move(direction);
    }
}
