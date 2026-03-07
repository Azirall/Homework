using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private List<BoxCollider> _spawnZones = new();
    [SerializeField] private int _initialPoolSize = 10;

    private readonly Queue<GameObject> _pool = new();
    private bool _isPrewarmed;
    private Func<Vector3> _playerPosDelegate;
    private EventBus _eventBus;

    public void Init(Func<Vector3> playerPosDelegate, EventBus eventBus)
    {
        _playerPosDelegate = playerPosDelegate;
        _eventBus = eventBus;
    }

    public void CreateEnemy(Action enemyKilledAction)
    {
        PrewarmPoolIfNeeded();

        GameObject enemy = GetFromPool();
        int randIndex = Random.Range(0, _spawnZones.Count);
        Vector3 spawnPos = _spawnZones[randIndex].GetSpawnPoint();

        enemy.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        EnemyBrain enemyBrain = enemy.GetComponent<EnemyBrain>();

        enemyHealth.SetKilledAction(() =>
        {
            ReturnToPool(enemy);
            enemyKilledAction?.Invoke();
        });
        enemyBrain.Init(_playerPosDelegate, _eventBus);

        enemy.SetActive(true);
    }

    private GameObject GetFromPool()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }

        return CreateNewEnemy();
    }

    private GameObject CreateNewEnemy()
    {
        Transform parent = _enemyContainer != null ? _enemyContainer : transform;
        Vector3 spawnPos = GetValidSpawnPosition();
        GameObject enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity, parent);
        return enemy;
    }

    private Vector3 GetValidSpawnPosition()
    {
        if (_spawnZones != null && _spawnZones.Count > 0)
        {
            int index = Random.Range(0, _spawnZones.Count);
            return _spawnZones[index].GetSpawnPoint();
        }
        Transform parent = _enemyContainer != null ? _enemyContainer : transform;
        return parent.position;
    }

    private void ReturnToPool(GameObject enemy)
    {
        if (enemy == null) return;

        enemy.SetActive(false);
        _pool.Enqueue(enemy);
    }

    private void PrewarmPoolIfNeeded()
    {
        if (_enemyPrefab == null) return;
        if (_isPrewarmed) return;

        for (int i = 0; i < _initialPoolSize; i++)
        {
            GameObject enemy = CreateNewEnemy();
            enemy.SetActive(false);
            _pool.Enqueue(enemy);
        }

        _isPrewarmed = true;
    }

    private void OnValidate()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab in wave manager is null", this);
        }

        if (_enemyContainer == null)
        {
            Debug.LogError("Enemy container in wave manager is null", this);
        }

        if (_spawnZones.Count == 0)
        {
            Debug.LogError("No spawn zones in wave manager", this);
        }
    }
}
