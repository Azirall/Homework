using UnityEngine;
public class KeyboardInputService : IInputService
{
    
    private readonly PlayerInputService _playerInputService;

    public KeyboardInputService(PlayerInputService playerInputService)
    {
        _playerInputService = playerInputService;
        _playerInputService.Player.Enable();
    }

    public Vector2 GetMoveDirection()
    {
        return _playerInputService.Player.Move.ReadValue<Vector2>();
    }
}
