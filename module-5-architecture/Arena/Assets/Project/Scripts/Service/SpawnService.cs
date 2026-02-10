using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpawnService
{
    private readonly SpawnServiceContext _context;
    private readonly List<GameObject> _activeEnemies = new();
    private readonly List<GameObject> _activeItems = new();

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
        _context.Runner.StartCoroutine(EnemySpawnCoroutine());
        _context.Runner.StartCoroutine(ItemSpawnCoroutine());
    }

    public void StopSpawning()
    {
        _context.Runner.StopAllCoroutines();
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
        GameObject enemyObj = _context.Pool.RentEnemy();
        
        if (enemyObj == null) return; 

        enemyObj.transform.position = _context.SpawnZone.GetRandomPointInside();

        var enemy = enemyObj.GetComponent<EnemyController>();
        float speed = _context.EnemyConfig.MoveSpeed;
        
        _activeEnemies.Add(enemyObj);
        enemy.Init(() =>{ _activeEnemies.Remove(enemyObj);_context.Pool.ReturnEnemy(enemyObj);});
        
        enemy.SetStrategy(new FollowPlayerRocketStrategy(_context.PlayerTransform,speed));
        
        enemyObj.SetActive(true);
        EventBus.RaiseGameEvent(new EnemySpawned());
        enemy.DieAfterSecond(_context.EnemyConfig.LifeTime);
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
