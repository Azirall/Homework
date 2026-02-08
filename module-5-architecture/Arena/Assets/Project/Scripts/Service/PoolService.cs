using System.Collections.Generic;
using UnityEngine;

public sealed class PoolService
{
    private readonly Queue<GameObject> _enemy = new();
    private readonly Queue<GameObject> _item = new();

    private GameObject _enemyPrefab;
    private GameObject _itemPrefab;
    private readonly Transform _poolContainer;
    public PoolService(EnemyConfig  enemyConfig, LootConfig lootConfig, Transform poolContainer)
    {
        _poolContainer = poolContainer;
        _enemyPrefab = enemyConfig.Prefab;
        _itemPrefab = lootConfig.Prefab;
    }

    public void WarmupEnemy(int count) => Warmup(_enemy, _enemyPrefab, _poolContainer, count);
    public void WarmupItem(int count)  => Warmup(_item,  _itemPrefab,  _poolContainer,  count);

    public GameObject RentEnemy() => Rent(_enemy);
    public GameObject RentItem()  => Rent(_item);

    public void ReturnEnemy(GameObject obj) => Return(_enemy, obj);
    public void ReturnItem(GameObject obj)  => Return(_item, obj);

    private static void Warmup(Queue<GameObject> q, GameObject prefab, Transform parent, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = UnityEngine.Object.Instantiate(prefab, parent);
            obj.SetActive(false);
            q.Enqueue(obj);
        }
    }

    private static GameObject Rent(Queue<GameObject> q)
    {
        if (q.Count == 0)
            return null;

        return q.Dequeue();
    }

    private static void Return(Queue<GameObject> q, GameObject obj)
    {
        obj.SetActive(false);
        q.Enqueue(obj);
    }
}