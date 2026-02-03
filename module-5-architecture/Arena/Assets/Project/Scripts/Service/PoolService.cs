using System.Collections.Generic;
using UnityEngine;

public sealed class PoolService
{
    private readonly Queue<GameObject> _enemy = new();
    private readonly Queue<GameObject> _loot = new();

    private GameObject _enemyPrefab;
    private GameObject _lootPrefab;
    private readonly Transform _poolContainer;
    public PoolService(EnemyConfig  enemyConfig, LootConfig lootConfig, Transform poolContainer)
    {
        _poolContainer = poolContainer;
        _enemyPrefab = enemyConfig.Prefab;
        _lootPrefab = lootConfig.Prefab;
    }

    public void WarmupEnemy(int count) => Warmup(_enemy, _enemyPrefab, _poolContainer, count);
    public void WarmupLoot(int count)  => Warmup(_loot,  _lootPrefab,  _poolContainer,  count);

    public GameObject RentEnemy() => Rent(_enemy, _enemyPrefab, _poolContainer);
    public GameObject RentLoot()  => Rent(_loot,  _lootPrefab,  _poolContainer);

    public void ReturnEnemy(GameObject obj) => Return(_enemy, obj);
    public void ReturnLoot(GameObject obj)  => Return(_loot, obj);

    private static void Warmup(Queue<GameObject> q, GameObject prefab, Transform parent, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = UnityEngine.Object.Instantiate(prefab, parent);
            obj.SetActive(false);
            q.Enqueue(obj);
        }
    }

    private static GameObject Rent(Queue<GameObject> q, GameObject prefab, Transform parent)
    {
        if (q.Count > 0) return q.Dequeue();
        var obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.SetActive(false);
        return obj;
    }

    private static void Return(Queue<GameObject> q, GameObject obj)
    {
        obj.SetActive(false);
        q.Enqueue(obj);
    }
}