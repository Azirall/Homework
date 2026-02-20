using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private GunConfig _gunData;
    
    private PlayerFacade _playerFacade;
    
    private InputSystem InputSystem => _sceneContext.InputSystem;
    private void Awake()
    {
        PlayerController playerController = _sceneContext.PlayerController;
        PlayerGun playerGun = _sceneContext.PlayerGun;
        PlayerHealth playerHealth = new();

        playerController.Init(InputSystem,_gameConfig);
        playerGun.Init(InputSystem,_gunData);
        
        _playerFacade = new PlayerFacade(playerController, playerGun, playerHealth);
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
    }
}
