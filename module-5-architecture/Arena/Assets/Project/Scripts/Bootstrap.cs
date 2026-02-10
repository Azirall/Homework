using System;
using System.Collections.Generic;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LootConfig _lootConfig;
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private SceneContext _sceneContext;
    
    private List<IDisposable>  _subscriptions = new();
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
        
        PoolService poolService = new PoolService(_enemyConfig, _lootConfig, _sceneContext.PoolContainer);
        
        var spawnContext = new SpawnServiceContext(_gameConfig, _enemyConfig, _lootConfig, poolService, this, 
                                                    _sceneContext.SpawnZone, _sceneContext.PlayerController.transform);
        
        _spawnService = new SpawnService(spawnContext);
        _stateMachine = new(_spawnService);
        _gameStateService = new GameStateService(_gameConfig, _stateMachine);
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
    
}

