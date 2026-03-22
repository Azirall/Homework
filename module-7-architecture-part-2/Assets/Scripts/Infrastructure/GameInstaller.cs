using System;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform[] _patrolPoints;

    [Header("UI")]
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private MonoBehaviour _gameOverView;
    [SerializeField] private MonoBehaviour _gameplayHud;

    private IInputService _inputService;
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        var logger = new DebugLoggerService();
        logger.GameStart();
        var healthService = new HealthService(_gameConfig.PlayerHealth, logger);
        SetupStateMachine(healthService);
        SetupInputService();
        SetupPlayerController(logger, healthService);
    }

    private void Start()
    {
        _stateMachine.ChangeState(typeof(MainMenuState));
    }

    private void SetupStateMachine(IHealthService healthService)
    {
        _stateMachine = new GameStateMachine();
        _stateMachine.RegisterState(typeof(MainMenuState), new MainMenuState(_mainMenuView, _stateMachine));
        _stateMachine.RegisterState(typeof(GameplayState), new GameplayState(healthService, _stateMachine, _pauseView));
        _stateMachine.RegisterState(typeof(PauseState), new PauseState(_pauseView, _stateMachine));
        _stateMachine.RegisterState(typeof(GameOverState), new GameOverState());
    }

    private void SetupPlayerController(ILoggerService logger, IHealthService healthService)
    {
        var movementService = new Rigidbody2DMovementService(_rigidbody2D);
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
            {
                Func<Vector2> getRigidbodyPosition = () => _rigidbody2D.position;
                _inputService = new AiInputService(_patrolPoints, getRigidbodyPosition);
                break;
            }
        }
    }

    private void OnValidate()
    {
        if (_playerController == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_playerController)} to be assigned.", this);

        if (_rigidbody2D == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_rigidbody2D)} to be assigned.", this);

        if (_gameConfig == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_gameConfig)} to be assigned.", this);

        if (_mainMenuView == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_mainMenuView)} to be assigned.", this);

        if (_pauseView == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_pauseView)} to be assigned.", this);
        else if (_pauseView is not IPauseView)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_pauseView)} to implement {nameof(IPauseView)}.", this);

        if (_gameOverView != null && _gameOverView is not IGameOverView)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_gameOverView)} to implement {nameof(IGameOverView)}.", this);

        if (_gameplayHud != null && _gameplayHud is not IGameplayHud)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_gameplayHud)} to implement {nameof(IGameplayHud)}.", this);
    }
}
