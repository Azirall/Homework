using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
        
    [SerializeField] private List<BoxCollider> _spawnZones = new();

    
    private Func<Vector3> _playerPosDelegate;
    
    public void Init(Func<Vector3> playerPosDelegate)
    {
        _playerPosDelegate = playerPosDelegate;
    }

    public void CreateEnemy( Action enemyKilledAction)
    {
            int randIndex = Random.Range(0, _spawnZones.Count);
            Vector3 spawnPos = _spawnZones[randIndex].GetSpawnPoint();
                
            GameObject enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
                
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
                
            enemyHealth.SetKilledAction(enemyKilledAction);
            enemyMove.Init(_playerPosDelegate);
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