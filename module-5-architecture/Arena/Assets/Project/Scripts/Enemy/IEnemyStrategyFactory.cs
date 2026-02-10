using UnityEngine;

public interface IEnemyStrategyFactory
{
    IRocketStrategy Create(EnemyConfig config, Transform spawnZone);
}
