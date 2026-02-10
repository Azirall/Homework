using System.Collections.Generic;
using UnityEngine;

public sealed class PoolService
{
    private readonly Dictionary<EnemyConfig, Queue<GameObject>> _enemyPools = new();
    private readonly Dictionary<GameObject, Queue<GameObject>> _enemyPoolByObject = new();
    private readonly Queue<GameObject> _item = new();
    private readonly List<EnemyConfig> _enemyConfigs = new();

    private readonly GameObject _itemPrefab;
    private readonly Transform _poolContainer;

    public PoolService(IReadOnlyList<EnemyConfig> enemyConfigs, LootConfig lootConfig, Transform poolContainer)
    {
        _itemPrefab = lootConfig.Prefab;
        _poolContainer = poolContainer;

        if (enemyConfigs == null)
        {
            return;
        }

        for (int i = 0; i < enemyConfigs.Count; i++)
        {
            var config = enemyConfigs[i];
            if (config == null || config.Prefab == null || _enemyPools.ContainsKey(config))
            {
                continue;
            }

            _enemyConfigs.Add(config);
            _enemyPools.Add(config, new Queue<GameObject>());
        }
    }

    public void WarmupEnemy(int count)
    {
        if (_enemyConfigs.Count == 0)
        {
            return;
        }

        int perType = count / _enemyConfigs.Count;
        int remainder = count % _enemyConfigs.Count;

        for (int i = 0; i < _enemyConfigs.Count; i++)
        {
            var config = _enemyConfigs[i];
            var pool = _enemyPools[config];

            int warmupCount = perType + (i < remainder ? 1 : 0);
            WarmupEnemyPool(pool, config.Prefab, warmupCount);
        }
    }

    public void WarmupItem(int count) => Warmup(_item, _itemPrefab, _poolContainer, count);

    public GameObject RentEnemy(EnemyConfig config)
    {
        if (config == null)
        {
            return null;
        }

        if (!_enemyPools.TryGetValue(config, out var pool))
        {
            return null;
        }

        return Rent(pool);
    }

    public GameObject RentItem() => Rent(_item);

    public void ReturnEnemy(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (_enemyPoolByObject.TryGetValue(obj, out var pool))
        {
            Return(pool, obj);
        }
    }

    public void ReturnItem(GameObject obj) => Return(_item, obj);

    private void WarmupEnemyPool(Queue<GameObject> pool, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = UnityEngine.Object.Instantiate(prefab, _poolContainer);
            obj.SetActive(false);
            pool.Enqueue(obj);
            _enemyPoolByObject[obj] = pool;
        }
    }

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
