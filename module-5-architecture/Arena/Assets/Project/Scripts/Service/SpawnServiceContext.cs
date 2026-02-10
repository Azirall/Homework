using System.Collections.Generic;
using UnityEngine;

public class SpawnServiceContext
{
    public GameConfig GameConfig { get; }
    public IReadOnlyList<EnemyConfig> EnemyConfigs { get; }
    public LootConfig LootConfig { get; }
    public PoolService Pool { get; }
    public IEnemyStrategyFactory StrategyFactory { get; }
    public MonoBehaviour Runner { get; }
    public Transform SpawnZone { get; }

    public SpawnServiceContext(
        GameConfig gameConfig,
        IReadOnlyList<EnemyConfig> enemyConfigs,
        LootConfig lootConfig,
        PoolService pool,
        IEnemyStrategyFactory strategyFactory,
        MonoBehaviour runner,
        Transform spawnZone)
    {
        GameConfig = gameConfig;
        EnemyConfigs = enemyConfigs;
        LootConfig = lootConfig;
        Pool = pool;
        StrategyFactory = strategyFactory;
        Runner = runner;
        SpawnZone = spawnZone;
    }
}
