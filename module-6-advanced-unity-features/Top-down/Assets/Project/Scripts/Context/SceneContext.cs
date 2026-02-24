using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerGun _playerGun;

    [Header("System")]
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private EnemyFactory _enemyFactory;
    
    public PlayerGun PlayerGun => _playerGun;
    public PlayerController PlayerController => _playerController;
    public InputSystem InputSystem => _inputSystem;
    public EnemyFactory EnemyFactory => _enemyFactory;

    private void OnValidate()
    {
        if (PlayerController == null)
        {
            Debug.LogError($"PlayerController in {name} is null");
        }

        if (PlayerGun == null)
        {
            Debug.LogError($"PlayerGun in {name} is null");
        }

        if (_inputSystem == null)
        {
            Debug.LogError($"InputSystem in {name} is null");
        }

        if (_enemyFactory == null)
        {
            Debug.LogError($"EnemyFactory in {name} is null");
        }

    }
}