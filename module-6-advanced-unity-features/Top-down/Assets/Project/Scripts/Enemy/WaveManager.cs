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
    private int _wavesRemaining;
    private int _currentEnemiesInWave;

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
        _wavesRemaining = _gameConfig.WavesCount;
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (_wavesRemaining <= 0 || _runner == null)
        {
            return;
        }

        _wavesRemaining--;
        _currentEnemiesInWave = _gameConfig.EnemiesInWave;

        if (_spawnCoroutine != null)
        {
            _runner.StopCoroutine(_spawnCoroutine);
        }

        _spawnCoroutine = _runner.StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        for (int j = 0; j < _gameConfig.EnemiesInWave; j++)
        {
            _enemyFactory.CreateEnemy(EnemyKilled);
            yield return new WaitForSeconds(_gameConfig.SpawnDelay);
        }
    }

    private void EnemyKilled()
    {
        _currentEnemiesInWave--;

        if (_currentEnemiesInWave <= 0)
        {
            _eventBus.RaiseGameEvent(new WaveDestroyed());
            StartNextWave();
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
