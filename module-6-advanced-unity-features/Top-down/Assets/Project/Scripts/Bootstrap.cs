using UnityEngine;

[RequireComponent(typeof(SceneContext))]
[RequireComponent(typeof(UiContext))]
[RequireComponent(typeof(GunContext))]

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private UiContext _uiContext;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private GunContext _gunContext;

    private InputSystem InputSystem => _sceneContext != null ? _sceneContext.InputSystem : null;

    private EventBus _eventBus;
    private PlayerHealth _playerHealth;
    private GameDirector _gameDirector;
    private PlayerArsenal _playerArsenal;
    private WaveManager _waveManager;
    private void Awake()
    {
        if (_sceneContext == null || _uiContext == null || _gameConfig == null)
        {
            Debug.LogError("Bootstrap contexts/config are not set");
            return;
        }

        PlayerController playerController = _sceneContext.PlayerController;
        EnemyFactory factory = _sceneContext.EnemyFactory;
        PlayerGun playerGun = _sceneContext.PlayerGun;
        
        _uiContext.GunPanelController.Init(_gunContext);
        
        if (playerController == null || factory == null || playerGun == null || InputSystem == null)
        {
            Debug.LogError("SceneContext has null references");
            return;
        }
        
        _eventBus = new EventBus();
        InputSystem.Init(_eventBus);
        _playerHealth = new PlayerHealth(10, _eventBus);
        _playerArsenal = new(_gunContext.GetConfigs(), _eventBus);
        
        factory.Init(() => playerController != null ? playerController.transform.position : Vector3.zero, _eventBus);
        
        playerController.Init(InputSystem, _gameConfig);
        playerGun.Init(InputSystem,_eventBus,_playerArsenal.GetDefaultGun());
        
        
        foreach (IEventUser consumer in _uiContext.EventConsumers)
        {
            if (consumer == null)
            {
                continue;
            }

            consumer.Subscribe(_eventBus);
        }
        
        _gameDirector = new GameDirector(_eventBus, _gameConfig);
        _waveManager = new WaveManager(_gameConfig, _eventBus, factory, this);
    }

    private void OnDestroy()
    {
        if (_uiContext != null)
        {
            foreach (IEventUser consumer in _uiContext.EventConsumers)
            {
                if (consumer == null)
                {
                    continue;
                }

                consumer.Unsubscribe();
            }
        }

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

    private void OnValidate()
    {
        if (_sceneContext == null)
        {
            Debug.LogError("SceneContext in Bootstrap is null");
        }

        if (_gameConfig == null)
        {
            Debug.LogError("GameConfig in Bootstrap is null");
        }

        if (_gunContext == null)
        {
            Debug.LogError("Gun context in Bootstrap is null");
        }

        if (_uiContext == null)
        {
            Debug.LogError("UiContext in Bootstrap is null");
        }
    }
}
