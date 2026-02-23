using System.Collections;
using UnityEngine;

public class WaveManager
{
    private EnemyFactory _enemyFactory;
    private GameConfig _gameConfig;
    private EventBus _eventBus;
    
    private MonoBehaviour _runner;

    public WaveManager(GameConfig gameConfig, EventBus eventBus, EnemyFactory factory, MonoBehaviour runner)
    {
        _runner = runner;
        _eventBus = eventBus;
        _enemyFactory = factory;
        _gameConfig = gameConfig;
        
        StartSpawn();
    }
    private void StartSpawn()
    {
        _runner.StartCoroutine(StartWaveSpawn());
    }
    private void EnemyKilled()
    {
        _eventBus.RaiseGameEvent(new EventTrigger(TriggerType.EnemyKilled));
    }

    IEnumerator StartWaveSpawn()
    {
        while (true)
        {
            _runner.StartCoroutine(SpawnEnemyCoroutine());
            yield return new WaitForSeconds(_gameConfig.TimeToNewWave);
        }
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        for (int j = 0; j < _gameConfig.EnemiesInWave; j++)
        {
            _enemyFactory.CreateEnemy(EnemyKilled);
            yield return new WaitForSeconds(_gameConfig.SpawnDelay);
        }
    }
}