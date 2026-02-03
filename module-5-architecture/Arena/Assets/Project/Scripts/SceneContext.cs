
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private Transform _spawnZone;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InputSystem _inputSystem;
    public PlayerController PlayerController => _playerController;
    public Transform PoolContainer => _poolContainer;
    public InputSystem InputSystem => _inputSystem;
    public Transform SpawnZone => _spawnZone;

    private void OnValidate()
    {
        if (_poolContainer == null)
        {
            Debug.LogError("Enemy container in Scene Context is null.");
        }

        if (_spawnZone == null)
        {
            Debug.LogError("Spawn zone in Scene Context is null.");
        }
    }
}