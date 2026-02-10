using System;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LootConfig _lootConfig;
    [SerializeField] private List<EnemyConfig> _enemyConfigs = new();
    [SerializeField] private SceneContext _sceneContext;

    private List<IDisposable> _subscriptions = new();
    private GameStateMachine _stateMachine;
    private GameStateService _gameStateService;
    private SpawnService _spawnService;
    private EventLogger _eventLogger;

    private void Awake()
    {
        if (_sceneContext == null)
        {
            Debug.LogError("Not all dependencies were resolved during bootstrap");
            return;
        }

        if (_enemyConfigs == null || _enemyConfigs.Count == 0)
        {
            Debug.LogError("Enemy configs list in Bootstrap is empty.");
            return;
        }

        PoolService poolService = new PoolService(_enemyConfigs, _lootConfig, _sceneContext.PoolContainer);
        IEnemyStrategyFactory enemyStrategyFactory = new EnemyStrategyFactory(_sceneContext.PlayerController.transform);

        var spawnContext = new SpawnServiceContext(
            _gameConfig,
            _enemyConfigs,
            _lootConfig,
            poolService,
            enemyStrategyFactory,
            this,
            _sceneContext.SpawnZone);

        _spawnService = new SpawnService(spawnContext);
        _stateMachine = new(_spawnService);
        _gameStateService = new GameStateService(_gameConfig, _stateMachine, _spawnService, _sceneContext.PlayerController);
        _eventLogger = new EventLogger();

        _eventLogger.Init();
        _sceneContext.LogPresenter.Init(_eventLogger);

        _sceneContext.PlayerController.Init(_sceneContext.InputSystem, _gameConfig);
        _gameStateService.Init();
    }

    private void Start()
    {
        _gameStateService.StartGame();
    }

    private void OnDestroy()
    {
        _gameStateService?.Dispose();
        _eventLogger?.Dispose();
    }
}
