using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform[] _patrolPoints;

    private IInputService _inputService;

    private void Awake()
    {
        var logger = new DebugLoggerService();
        logger.GameStart();
        SetupInputService();
        SetupPlayerController(logger);
    }

    private void SetupPlayerController(ILoggerService logger)
    {
        var movementService = new Rigidbody2DMovementService(_rigidbody2D);
        var healthService = new HealthService(_gameConfig.PlayerHealth);
        var coinWalletService = new CoinWalletService();
        _playerController.Initialize(_inputService, movementService, _gameConfig, healthService, coinWalletService, logger);
    }

    private void SetupInputService()
    {
        switch (_gameConfig.InputSource)
        {
            case InputSourceKind.Keyboard:
                _inputService = new KeyboardInputService(new PlayerInputService());
                break;
            case InputSourceKind.AIInputService:
                break;
        }
    }

    private void OnValidate()
    {
        if (_playerController == null)
            Debug.LogError($"{nameof(Bootstrap)} requires {nameof(_playerController)} to be assigned.", this);

        if (_rigidbody2D == null)
            Debug.LogError($"{nameof(Bootstrap)} requires {nameof(_rigidbody2D)} to be assigned.", this);

        if (_gameConfig == null)
            Debug.LogError($"{nameof(Bootstrap)} requires {nameof(_gameConfig)} to be assigned.", this);
    }
}
