using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private UiContext _uiContext;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private GunConfig _gunData;
    private InputSystem InputSystem => _sceneContext.InputSystem;
    private void Awake()
    {
        PlayerController playerController = _sceneContext.PlayerController;
        EnemyFactory factory = _sceneContext.EnemyFactory;
        PlayerGun playerGun = _sceneContext.PlayerGun;
        EventBus eventBus = new();
        
        factory.Init(() => playerController.transform.position);
        playerController.Init(InputSystem,_gameConfig);
        playerGun.Init(InputSystem);
        
        
        foreach (IEventConsumer consumer in _uiContext.EventConsumers)
        {
            consumer.Subscribe(eventBus);
        }
        
        GameDirector gameDirector = new GameDirector(eventBus, _gameConfig);
        WaveManager waveManager = new WaveManager(_gameConfig, eventBus, factory,this);
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

        if (_gunData == null)
        {
            Debug.LogError("GunConfig in Bootstrap is null");
        }

        if (_uiContext == null)
        {
            Debug.LogError("UiContext in Bootstrap is null");
        }
    }
}
