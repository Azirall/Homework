using UnityEngine;

public class SpawnServiceContext
{
    public GameConfig GameConfig { get; }
    public EnemyConfig EnemyConfig { get; }
    public LootConfig LootConfig { get; }
    public PoolService Pool { get; }
    public MonoBehaviour Runner { get; }
    public Transform SpawnZone { get; }
    public Transform PlayerTransform { get; }

    public SpawnServiceContext(
        GameConfig gameConfig,
        EnemyConfig enemyConfig,
        LootConfig lootConfig,
        PoolService pool,
        MonoBehaviour runner,
        Transform spawnZone,
        Transform playerTransform)
    {
        GameConfig = gameConfig;
        EnemyConfig = enemyConfig;
        LootConfig = lootConfig;
        Pool = pool;
        Runner = runner;
        SpawnZone = spawnZone;
        PlayerTransform = playerTransform;
    }
}
