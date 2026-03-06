using System;
using UnityEngine;

public class GameCompositionRoot : IDisposable
{
    private readonly SceneContext _sceneContext;
    private readonly UiContext _uiContext;
    private readonly GameConfig _gameConfig;
    private readonly GunContext _gunContext;
    private readonly MonoBehaviour _runner;

    private EventBus _eventBus;
    private PlayerHealth _playerHealth;
    private PlayerArsenal _playerArsenal;
    private GameDirector _gameDirector;
    private WaveManager _waveManager;
    private PlayerInitializer _playerInitializer;
    private UiInitializer _uiInitializer;

    public GameCompositionRoot(SceneContext sceneContext, UiContext uiContext, GameConfig gameConfig, GunContext gunContext, MonoBehaviour runner)
    {
        _sceneContext = sceneContext;
        _uiContext = uiContext;
        _gameConfig = gameConfig;
        _gunContext = gunContext;
        _runner = runner;
        Debug.Log("GameCompositionRoot initialized");
    }

    public void Init()
    {
        if (_sceneContext == null || _uiContext == null || _gameConfig == null || _gunContext == null)
        {
            Debug.LogError("GameCompositionRoot: one of required contexts/configs is null");
            return;
        }

        PlayerController playerController = _sceneContext.PlayerController;
        EnemyFactory factory = _sceneContext.EnemyFactory;
        PlayerGun playerGun = _sceneContext.PlayerGun;
        InputSystem inputSystem = _sceneContext.InputSystem;

        if (playerController == null || factory == null || playerGun == null || inputSystem == null)
        {
            Debug.LogError("GameCompositionRoot: SceneContext has null references");
            return;
        }

        _eventBus = new EventBus();
        
        _uiInitializer = new UiInitializer(_uiContext, _gunContext, _eventBus);
        _uiInitializer.Init();
        
        _playerHealth = new PlayerHealth(10, _eventBus);
        _playerArsenal = new PlayerArsenal(_gunContext.GetConfigs(), _eventBus);

        _playerInitializer = new PlayerInitializer(inputSystem, playerController, playerGun, _gameConfig, _eventBus, _playerArsenal);
        _playerInitializer.Init();

        factory.Init(() => playerController != null ? playerController.transform.position : Vector3.zero, _eventBus);

        _waveManager = new WaveManager(_gameConfig, _eventBus, factory, _runner);
        _gameDirector = new GameDirector(_eventBus, _gameConfig);
    }

    public void Dispose()
    {
        _uiInitializer?.Dispose();
        _uiInitializer = null;

        _playerHealth?.Dispose();
        _playerHealth = null;

        _gameDirector?.Dispose();
        _gameDirector = null;

        _waveManager?.Dispose();
        _waveManager = null;

        _playerArsenal?.Dispose();
        _playerArsenal = null;

        _eventBus = null;
    }
}

