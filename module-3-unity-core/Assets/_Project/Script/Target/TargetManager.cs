using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TargetManager: MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private BoxCollider _spawnZone;
    [SerializeField] private float _spawnDelay = 4f;
    [SerializeField] private float _targetLifeTime = 3f;
    private int _targetSpawn = 0;
    
    private void Start()
    {
        StartCoroutine(nameof(TargetSpawn));
        
    }

    private IEnumerator TargetSpawn()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomPointInBox();

            Target target = Instantiate(_targetPrefab, spawnPosition, Quaternion.identity).GetComponent<Target>();
            target.Init(_targetLifeTime);
            _targetSpawn++;
            Debug.Log($"Target spawn count: {_targetSpawn}");
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
            Debug.LogWarning("Зона спавна не выбрана");
        }
        if (_targetPrefab == null)
        {
            Debug.LogWarning("Не назначен префаб цели");
        }
    }
}

