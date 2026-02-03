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
    private GameStateService _gameStateService;
    private SpawnService _spawnService;
    private void Awake()
    {
        if (_sceneContext == null)
        {
            Debug.LogError("Not all dependencies were resolved during bootstrap");
            return;
        } 
        PoolService _poolService = new PoolService(_enemyConfig,_lootConfig, _sceneContext.PoolContainer);
        
        _spawnService = new SpawnService(_enemyConfig, _gameConfig, _lootConfig, _poolService, this, _sceneContext.SpawnZone);
        
        _gameStateService = new GameStateService(_gameConfig);
        
        _sceneContext.PlayerController.Init(_sceneContext.InputSystem);
        _gameStateService.Init();
        
    }

    private void Start()
    {
        _gameStateService.StartGame();
    }
    
}

