using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
        
    [SerializeField] private List<BoxCollider> _spawnZones = new();

    
    private Func<Vector3> _playerPosDelegate;
    private EventBus _eventBus;
    public void Init(Func<Vector3> playerPosDelegate, EventBus eventBus)
    {
        _playerPosDelegate = playerPosDelegate;
        _eventBus = eventBus;
    }

    public void CreateEnemy( Action enemyKilledAction)
    {
            int randIndex = Random.Range(0, _spawnZones.Count);
            Vector3 spawnPos = _spawnZones[randIndex].GetSpawnPoint();
                
            GameObject enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
                
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            EnemyBrain enemyBrain = enemy.GetComponent<EnemyBrain>();
            
            enemyHealth.SetKilledAction(enemyKilledAction);
            enemyBrain.Init(_playerPosDelegate, _eventBus);
        }
    private void OnValidate()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab in wave manager is null");
        }

        if (_spawnZones.Count == 0)
        {
            Debug.LogError("No spawn zones in wave manager");
        }
    }
}