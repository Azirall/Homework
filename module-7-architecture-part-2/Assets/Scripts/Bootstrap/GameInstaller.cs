using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Game")]
    [SerializeField] private GameConfig _gameConfig;

    [Header("Coin Spawning")]
    [SerializeField] private GameObject _coinPickupPrefab;
    [SerializeField] private CoinConfig _coinConfig;
    [SerializeField] private List<BoxCollider2D> _spawnZones;

    [Header("AI Patrol")]
    [SerializeField] private Transform[] _patrolPoints;

    [Header("Modifiers")]
    [SerializeField] private List<GameModifierConfig> _gameModifiers = new List<GameModifierConfig>();

    [Header("UI")]
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GameplayHudView _gameplayHud;

    private IInputService _inputService;
    private IMovementBlocker _movementBlocker;
    private DamageMultiplierService _damageMultiplierService;
    private GameStateMachine _stateMachine;
    private GameController _gameController;
    private ISpawnController _coinSpawnController;
    private void Awake()
    {
        var logger = new DebugLoggerService();
        logger.GameStart();
        
        var healthService = new HealthService(_gameConfig.PlayerHealth, logger);
        _damageMultiplierService = new DamageMultiplierService(healthService);
        var coinWalletService = new CoinWalletService();

        SetupCoinSpawn();

        SetupInputService();
        SetupStateMachine(healthService, logger);
        SetupPlayerController(logger, _damageMultiplierService, coinWalletService);
        SetupGameplayHud(healthService, coinWalletService);
    }

    private void Start()
    {
        if (StartGameplayAfterSceneLoad)
        {
            StartGameplayAfterSceneLoad = false;
            _mainMenuView.HidePanel();
            _gameController.StartGameplay();
        }
        else
            _gameController.StartGame();
    }

    private void Update()
    {
        _gameController.Tick();
    }

    private void SetupStateMachine(IHealthService healthService, ILoggerService logger)
    {
        var modifierFactory = new GameModifierFactory(_gameModifiers, healthService, _movementBlocker,_damageMultiplierService);

        _stateMachine = new GameStateMachine(logger);
        _gameController = new GameController(_stateMachine);
        _stateMachine.RegisterState(typeof(MainMenuState), new MainMenuState(_mainMenuView, _gameController));
        _stateMachine.RegisterState(typeof(GameplayState),
            new GameplayState(healthService, _gameController, _pauseView, _coinSpawnController, modifierFactory.Modifiers));
        _stateMachine.RegisterState(typeof(PauseState), new PauseState(_pauseView, _gameController));
        _stateMachine.RegisterState(typeof(GameOverState), new GameOverState(_gameOverView));
    }

    private void SetupPlayerController(ILoggerService logger, IHealthService healthService, ICoinWalletService coinWalletService)
    {
        var movementService = new Rigidbody2DMovementService(_rigidbody2D);
        _playerController.Initialize(_inputService, movementService, _gameConfig, healthService, coinWalletService, logger);
    }

    private void SetupGameplayHud(IHealthService healthService, ICoinWalletService coinWalletService)
    {
        if (_gameplayHud == null)
            return;

        _gameplayHud.SetHealth(_gameConfig.PlayerHealth);
        _gameplayHud.SetScore(coinWalletService.Balance);
        _gameplayHud.SetCurrentInputMode(_gameConfig.InputSource);

        healthService.HealthChanged += _gameplayHud.SetHealth;
        coinWalletService.BalanceChanged += _gameplayHud.SetScore;
    }

    private void SetupInputService()
    {
        switch (_gameConfig.InputSource)
        {
            case InputSourceKind.Keyboard:
            {
                var keyboardInput = new MovementBlockingInputService(new KeyboardInputService(new PlayerInputService()));
                _inputService = keyboardInput;
                _movementBlocker = keyboardInput;
                break;
            }
            case InputSourceKind.AIInputService:
            {
                Vector2 GetRigidbodyPosition() => _rigidbody2D.position;
                var aiInput = new MovementBlockingInputService(new AiInputService(_patrolPoints, GetRigidbodyPosition));
                _inputService = aiInput;
                _movementBlocker = aiInput;
                break;
            }
        }
    }

    private void SetupCoinSpawn()
    {
        var poolFactory = new GameObjectPoolFactory();
        _coinSpawnController = new SpawnController();

        poolFactory.Init(_coinPickupPrefab, _coinConfig);
        _coinSpawnController.Init(_spawnZones, poolFactory, _gameConfig.SpawnInterval);
    }

    private void OnValidate()
    {
        if (_playerController == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_playerController)} to be assigned.", this);

        if (_rigidbody2D == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_rigidbody2D)} to be assigned.", this);

        if (_gameConfig == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_gameConfig)} to be assigned.", this);

        if (_coinPickupPrefab == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_coinPickupPrefab)} to be assigned.", this);

        if (_coinConfig == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_coinConfig)} to be assigned.", this);

        if (_spawnZones == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_spawnZones)} to be assigned.", this);

        if (_mainMenuView == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_mainMenuView)} to be assigned.", this);

        if (_pauseView == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_pauseView)} to be assigned.", this);

        if (_gameOverView == null)
            Debug.LogError($"{nameof(GameInstaller)} requires {nameof(_gameOverView)} to be assigned.", this);
    }
}
