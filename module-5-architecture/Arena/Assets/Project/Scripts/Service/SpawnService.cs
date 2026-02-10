using System.Collections;
using UnityEngine;

public sealed class SpawnService
{
    private readonly SpawnServiceContext _context;

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

    private IEnumerator EnemySpawnCoroutine()
    {
        var delay = new WaitForSeconds(_context.GameConfig.EnemyRespawnTime);

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
        
        enemy.Init(() => _context.Pool.ReturnEnemy(enemyObj));
        
        
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
        
        item.Init(() => _context.Pool.ReturnItem(itemObj));
        
        itemObj.SetActive(true);
    }
}
