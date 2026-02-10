using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyStrategyFactory : IEnemyStrategyFactory
{
    private const int PatrolPointsCount = 3;

    private readonly Transform _playerTransform;

    public EnemyStrategyFactory(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public IRocketStrategy Create(EnemyConfig config, Transform spawnZone)
    {
        return config.StrategyType switch
        {
            EnemyStrategyType.Chase => new FollowPlayerRocketStrategy(_playerTransform, config.MoveSpeed),
            EnemyStrategyType.Patrol => new PatrolRocketStrategy(BuildPatrolPoints(spawnZone), config.MoveSpeed),
            _ => new FollowPlayerRocketStrategy(_playerTransform, config.MoveSpeed)
        };
    }

    private static List<Vector2> BuildPatrolPoints(Transform spawnZone)
    {
        var patrolPoints = new List<Vector2>(PatrolPointsCount);

        for (int i = 0; i < PatrolPointsCount; i++)
        {
            patrolPoints.Add(spawnZone.GetRandomPointInside());
        }

        return patrolPoints;
    }
}
