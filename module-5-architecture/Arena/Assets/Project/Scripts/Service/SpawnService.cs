using System;
using System.Collections;
using UnityEngine;

public sealed class SpawnService : IDisposable
{
    private readonly GameConfig _gameConfig;
    private readonly EnemyConfig _enemyConfig;
    private readonly MonoBehaviour _runner;
    private readonly PoolService _pool;
    private readonly LootConfig _lootConfig;
    
    private Coroutine _spawnCoroutine;
    private Transform _spawnZone;
    public SpawnService(EnemyConfig enemyConfig, GameConfig gameConfig,LootConfig lootConfig, PoolService pool, MonoBehaviour runner, Transform spawnZone)
    {
        _enemyConfig = enemyConfig;
        _gameConfig = gameConfig;
        _lootConfig = lootConfig;
        _pool = pool;
        _runner = runner;
        _spawnZone = spawnZone;
        
        EventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent e)
    {
        if (e is not GameStateChanged s) return;

        switch (s.GameState)
        {
            case GameState.Init:
                _pool.WarmupEnemy(_gameConfig.MaxEnemy);
                _pool.WarmupLoot(_lootConfig.LootItemAmount);
                break;

            case GameState.Playing:
                StartSpawning();
                break;

            case GameState.Paused:
            case GameState.Win:
                StopSpawning();
                break;
        }
    }

    private void StartSpawning()
    {
        if (_spawnCoroutine != null) return;
        _spawnCoroutine = _runner.StartCoroutine(SpawnCoroutine());
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine == null) return;
        _runner.StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnCoroutine()
    {
        var delay = new WaitForSeconds(_gameConfig.EnemyRespawnTime);

        while (true)
        {
            SpawnOneEnemy();
            yield return delay;
        }
    }

    private void SpawnOneEnemy()
    {
        GameObject enemyObj = _pool.RentEnemy();
        
        enemyObj.transform.position = _spawnZone.GetRandomPointInside();

        var enemy = enemyObj.GetComponent<EnemyController>();

        enemy.Init(() => _pool.ReturnEnemy(enemyObj), _enemyConfig.MoveSpeed);

        enemyObj.SetActive(true);

        enemy.DieAfterSecond(_enemyConfig.LifeTime);
    }

    public void Dispose()
    {
        StopSpawning();
        EventBus.OnGameEvent -= HandleEvent;
    }
}
