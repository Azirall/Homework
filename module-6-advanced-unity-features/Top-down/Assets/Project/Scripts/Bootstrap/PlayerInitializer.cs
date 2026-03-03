using UnityEngine;

public class PlayerInitializer
{
    private readonly InputSystem _inputSystem;
    private readonly PlayerController _playerController;
    private readonly PlayerGun _playerGun;
    private readonly GameConfig _gameConfig;
    private readonly EventBus _eventBus;
    private readonly PlayerArsenal _playerArsenal;

    public PlayerInitializer(InputSystem inputSystem,
        PlayerController playerController,
        PlayerGun playerGun,
        GameConfig gameConfig,
        EventBus eventBus,
        PlayerArsenal playerArsenal)
    {
        _inputSystem = inputSystem;
        _playerController = playerController;
        _playerGun = playerGun;
        _gameConfig = gameConfig;
        _eventBus = eventBus;
        _playerArsenal = playerArsenal;
    }

    public void Init()
    {
        if (_inputSystem == null || _playerController == null || _playerGun == null || _gameConfig == null || _eventBus == null || _playerArsenal == null)
        {
            Debug.LogError("PlayerInitializer: one of required dependencies is null");
            return;
        }

        _inputSystem.Init(_eventBus);
        _playerController.Init(_inputSystem, _gameConfig);
        _playerGun.Init(_inputSystem, _eventBus, _playerArsenal.GetDefaultGun());
    }
}

