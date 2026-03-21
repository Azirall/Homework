using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    private IInputService _inputService;
    private GameConfig _gameConfig;

    public void Initialize(IInputService inputService, IMovementService movementService, GameConfig gameConfig)
    {
        _inputService = inputService;
        _gameConfig = gameConfig;
        _playerMovement.Initialize(movementService);
    }

    private void FixedUpdate()
    {
        if (_inputService == null)
            return;

        _playerMovement.Move(_inputService.GetMoveDirection() * _gameConfig.MoveSpeed);
    }

    private void OnValidate()
    {
        if (_playerMovement == null) _playerMovement = GetComponent<PlayerMovement>();
    }
}
