using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpawnService
{
    private readonly SpawnServiceContext _context;
    private readonly List<GameObject> _activeEnemies = new();
    private readonly List<GameObject> _activeItems = new();
    private Coroutine _enemySpawnCoroutine;
    private Coroutine _itemSpawnCoroutine;

    public SpawnService(SpawnServiceContext context)
    {
        _context = context;
    }

    public void CreateObjectsInPool()
    {
        _context.Pool.WarmupEnemy(_context.GameConfig.MaxEnemy);
        _context.Pool.WarmupItem(_context.LootConfig.LootItemAmount);
    }

    public void StartSpawning()
    {
        if (_enemySpawnCoroutine == null)
        {
            _enemySpawnCoroutine = _context.Runner.StartCoroutine(EnemySpawnCoroutine());
        }

        if (_itemSpawnCoroutine == null)
        {
            _itemSpawnCoroutine = _context.Runner.StartCoroutine(ItemSpawnCoroutine());
        }
    }

    public void StopSpawning()
    {
        if (_enemySpawnCoroutine != null)
        {
            _context.Runner.StopCoroutine(_enemySpawnCoroutine);
            _enemySpawnCoroutine = null;
        }

        if (_itemSpawnCoroutine != null)
        {
            _context.Runner.StopCoroutine(_itemSpawnCoroutine);
            _itemSpawnCoroutine = null;
        }
    }

    public void ResetAllSpawnedObjects()
    {
        StopSpawning();

        foreach (var enemy in _activeEnemies)
        {
            if (enemy != null)
            {
                _context.Pool.ReturnEnemy(enemy);
            }
        }
        _activeEnemies.Clear();

        foreach (var item in _activeItems)
        {
            if (item != null)
            {
                _context.Pool.ReturnItem(item);
            }
        }
        _activeItems.Clear();
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        var delay = new WaitForSeconds(_context.GameConfig.EnemyRespawnTime);

        yield return new WaitForSeconds(2f);
        while (true)
        {
            SpawnOneEnemy();
            yield return delay;
        }
    }

    private IEnumerator ItemSpawnCoroutine()
    {
        var delay = new WaitForSeconds(_context.LootConfig.LootSpawnTime);

        while (true)
        {
            SpawnOneItem();
            yield return delay;
        }
    }

    private void SpawnOneEnemy()
    {
        if (!TryRentEnemy(out var enemyConfig, out var enemyObj))
        {
            return;
        }

        enemyObj.transform.position = _context.SpawnZone.GetRandomPointInside();

        var enemy = enemyObj.GetComponent<EnemyController>();

        _activeEnemies.Add(enemyObj);
        enemy.Init(() =>
        {
            _activeEnemies.Remove(enemyObj);
            _context.Pool.ReturnEnemy(enemyObj);
        });

        enemy.SetStrategy(_context.StrategyFactory.Create(enemyConfig, _context.SpawnZone));

        enemyObj.SetActive(true);
        EventBus.RaiseGameEvent(new EnemySpawned());
        enemy.DieAfterSecond(enemyConfig.LifeTime);
    }

    private bool TryRentEnemy(out EnemyConfig enemyConfig, out GameObject enemyObj)
    {
        enemyConfig = null;
        enemyObj = null;

        var configs = _context.EnemyConfigs;
        if (configs == null || configs.Count == 0)
        {
            return false;
        }

        int startIndex = Random.Range(0, configs.Count);

        for (int i = 0; i < configs.Count; i++)
        {
            var candidate = configs[(startIndex + i) % configs.Count];
            if (candidate == null)
            {
                continue;
            }

            var rented = _context.Pool.RentEnemy(candidate);
            if (rented == null)
            {
                continue;
            }

            enemyConfig = candidate;
            enemyObj = rented;
            return true;
        }

        return false;
    }

    private void SpawnOneItem()
    {
        GameObject itemObj = _context.Pool.RentItem();

        if (itemObj == null) return;

        itemObj.transform.position = _context.SpawnZone.GetRandomPointInside();
        PickupItem item = itemObj.GetComponent<PickupItem>();

        _activeItems.Add(itemObj);
        item.Init(() =>
        {
            _activeItems.Remove(itemObj);
            _context.Pool.ReturnItem(itemObj);
        });

        itemObj.SetActive(true);
    }
}
