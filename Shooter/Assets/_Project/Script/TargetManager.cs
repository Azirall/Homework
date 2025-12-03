using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetManager: MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private BoxCollider spawnZone;
    [SerializeField] private float spawnDelay = 4f;
    [SerializeField] private float targetLifeTime = 3f;
    private void Start()
    {
        StartCoroutine(nameof(TargetSpawn));
    }

    private IEnumerator TargetSpawn()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomPointInBox();

            Target target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity).GetComponent<Target>();
            target.Init(targetLifeTime);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private Vector3 GetRandomPointInBox()
    {
        Vector3 localPos = new Vector3(
            Random.Range(-spawnZone.size.x / 2f, spawnZone.size.x / 2f),
            Random.Range(-spawnZone.size.y / 2f, spawnZone.size.y / 2f),
            Random.Range(-spawnZone.size.z / 2f, spawnZone.size.z / 2f)
        );
        
        localPos += spawnZone.center;
        
        return spawnZone.transform.TransformPoint(localPos);
    }


    private void OnValidate()
    {
        if (spawnZone == null)
        {
            Debug.LogWarning("Зона спавна не выбрана");
        }
        if (targetPrefab == null)
        {
            Debug.LogWarning("Не назначен префаб цели");
        }
    }
}

