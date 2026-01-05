using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class TargetManager: MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private BoxCollider _spawnZone;
    [SerializeField] private float _spawnDelay = 4f;
    [SerializeField] private float _targetLifeTime = 3f;
    
    private TargetStats _targetStats;
    private DiContainer _diContainer;

    [Inject]
    private void Construct(TargetStats targetStats, DiContainer diContainer)
    {
        _diContainer = diContainer;
        _targetStats = targetStats;
    }
    
    private void Start()
    {
        StartCoroutine(nameof(TargetSpawn));
        
    }

    private IEnumerator TargetSpawn()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomPointInBox();

            Target target = _diContainer.InstantiatePrefab(_targetPrefab, spawnPosition, Quaternion.identity, null).GetComponent<Target>();
            
            _targetStats.AddSpawnedTarget();
            target.Init(_targetLifeTime);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
    private Vector3 GetRandomPointInBox()
    {
        Vector3 localPos = new Vector3(
            Random.Range(-_spawnZone.size.x / 2f, _spawnZone.size.x / 2f),
            Random.Range(-_spawnZone.size.y / 2f, _spawnZone.size.y / 2f),
            Random.Range(-_spawnZone.size.z / 2f, _spawnZone.size.z / 2f)
        );
        
        localPos += _spawnZone.center;
        
        return _spawnZone.transform.TransformPoint(localPos);
    }


    private void OnValidate()
    {
        if (_spawnZone == null)
        {
            Debug.LogError("Spawn Zone is missing");
        }
        if (_targetPrefab == null)
        {
            Debug.LogError("Target Prefab is missing");
        }
    }
}

