using System;
using System.Collections;
using UnityEngine;

public class WaveManager : IDisposable
{
    private readonly EnemyFactory _enemyFactory;
    private readonly GameConfig _gameConfig;
    private EventBus _eventBus;
    private readonly MonoBehaviour _runner;
    private Coroutine _spawnCoroutine;
    private bool _isSpawnStarted;

    public WaveManager(GameConfig gameConfig, EventBus eventBus, EnemyFactory factory, MonoBehaviour runner)
    {
        _runner = runner;
        _eventBus = eventBus;
        _enemyFactory = factory;
        _gameConfig = gameConfig;
        _eventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is GameStateChanged { State: GameStateType.Playing })
        {
            StartSpawn();
        }
    }

    private void StartSpawn()
    {
        if (_isSpawnStarted || _runner == null)
        {
            return;
        }

        _isSpawnStarted = true;
        _spawnCoroutine = _runner.StartCoroutine(StartWaveSpawn());
    }

    private void EnemyKilled()
    {
        _eventBus.RaiseGameEvent(new EventTrigger(TriggerType.EnemyKilled));
    }

    private IEnumerator StartWaveSpawn()
    {
        while (true)
        {
            _runner.StartCoroutine(SpawnEnemyCoroutine());
            yield return new WaitForSeconds(_gameConfig.TimeToNewWave);
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        for (int j = 0; j < _gameConfig.EnemiesInWave; j++)
        {
            _enemyFactory.CreateEnemy(EnemyKilled);
            yield return new WaitForSeconds(_gameConfig.SpawnDelay);
        }
    }

    public void Dispose()
    {
        if (_eventBus == null)
        {
            return;
        }

        _eventBus.OnGameEvent -= HandleEvent;

        if (_spawnCoroutine != null && _runner != null)
        {
            _runner.StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        _eventBus = null;
    }
}
