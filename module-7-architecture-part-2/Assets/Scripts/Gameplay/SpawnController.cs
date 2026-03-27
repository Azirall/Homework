using System.Collections.Generic;
using UnityEngine;

public class SpawnController : ISpawnController
{
    private IReadOnlyList<BoxCollider2D> _spawnZones;
    private IGameObjectPoolFactory _poolFactory;
    private float _spawnInterval;
    private float _nextSpawnTime;

    public void Init(IReadOnlyList<BoxCollider2D> spawnZones, IGameObjectPoolFactory poolFactory, float spawnInterval)
    {
        _spawnZones = spawnZones;
        _poolFactory = poolFactory;
        _spawnInterval = spawnInterval;
        _nextSpawnTime = Time.time + _spawnInterval;
    }

    public void Tick()
    {
        if (Time.time < _nextSpawnTime)
            return;

        var spawnZone = _spawnZones[Random.Range(0, _spawnZones.Count)];
        var spawnPoint = spawnZone.GetRandomPointInZone();

        GameObject poolObject = _poolFactory.GetFromPool();
        
        poolObject.transform.position = spawnPoint;
        
        poolObject.SetActive(true);
        
        _nextSpawnTime = Time.time + _spawnInterval;
    }
}
