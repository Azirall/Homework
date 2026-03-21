using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInteraction _playerInteraction;
    private IInputService _inputService;
    private GameConfig _gameConfig;
    public void Initialize(IInputService inputService, IMovementService movementService, GameConfig gameConfig, HealthService healthService, ICoinWalletService coinWalletService, ILoggerService logger)
    {
        _inputService = inputService;
        _gameConfig = gameConfig;
        _playerMovement.Initialize(movementService);
        _playerInteraction.Initialize(healthService, coinWalletService, logger);
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
        if (_playerInteraction == null) _playerInteraction = GetComponent<PlayerInteraction>();
    }
}
