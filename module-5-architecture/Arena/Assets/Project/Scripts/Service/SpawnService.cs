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
    
    private Transform _playerTransform;
    private Transform _spawnZone;
    public SpawnService(EnemyConfig enemyConfig, GameConfig gameConfig,LootConfig lootConfig, PoolService pool,
                        MonoBehaviour runner, Transform spawnZone, Transform playerTransform)
    {
        _enemyConfig = enemyConfig;
        _gameConfig = gameConfig;
        _lootConfig = lootConfig;
        _pool = pool;
        _runner = runner;
        _spawnZone = spawnZone;
        _playerTransform = playerTransform;
        
        EventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent e)
    {
        if (e is not GameStateChanged s) return;

        switch (s.GameState)
        {
            case GameState.Init:
                _pool.WarmupEnemy(_gameConfig.MaxEnemy);
                _pool.WarmupItem(_lootConfig.LootItemAmount);
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
        _runner.StartCoroutine(EnemySpawnCoroutine());
        _runner.StartCoroutine(ItemSpawnCoroutine());
    }

    private void StopSpawning()
    {
        _runner.StopAllCoroutines();
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        var delay = new WaitForSeconds(_gameConfig.EnemyRespawnTime);

        while (true)
        {
            SpawnOneEnemy();
            yield return delay;
        }
    }

    private IEnumerator ItemSpawnCoroutine()
    {
        var delay = new WaitForSeconds(_lootConfig.LootSpawnTime);

        while (true)
        {
            SpawnOneItem();
            yield return delay;
        }
        
    }

    private void SpawnOneEnemy()
    {
        GameObject enemyObj = _pool.RentEnemy();
        
        if(enemyObj == null) return; 
        
        enemyObj.transform.position = _spawnZone.GetRandomPointInside();

        var enemy = enemyObj.GetComponent<EnemyController>();
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        enemy.Init(() => _pool.ReturnEnemy(enemyObj));
        
        enemy.SetStrategy(new FollowPlayerRocketStrategy(rb, _playerTransform,_enemyConfig.MoveSpeed));
        
        enemyObj.SetActive(true);

        enemy.DieAfterSecond(_enemyConfig.LifeTime);
    }

    private void SpawnOneItem()
    {
        GameObject itemObj = _pool.RentItem();
        
        if(itemObj == null) return;
        
        itemObj.transform.position = _spawnZone.GetRandomPointInside();
        PickupItem item = itemObj.GetComponent<PickupItem>();
        
        item.Init(() => _pool.ReturnItem(itemObj));
        
        itemObj.SetActive(true);
    }

    public void Dispose()
    {
        StopSpawning();
        EventBus.OnGameEvent -= HandleEvent;
    }
}
