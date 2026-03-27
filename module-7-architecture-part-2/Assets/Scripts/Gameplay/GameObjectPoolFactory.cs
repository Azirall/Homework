using System.Collections.Generic;
using UnityEngine;

public sealed class GameObjectPoolFactory : IGameObjectPoolFactory
{
    private const int InitialPoolSize = 5;

    private readonly Queue<GameObject> _pool = new();
    private GameObject _prefab;
    private ScriptableObject _config;

    public void Init(GameObject prefab, ScriptableObject config)
    {
        _prefab = prefab;
        _config = config;
        EnsurePool();
    }

    public GameObject GetFromPool()
    {
        EnsurePool();
        if (_pool.Count == 0)
            _pool.Enqueue(CreatePooledInstance());

        var pooledObject = _pool.Dequeue();
        if (pooledObject.TryGetComponent<IPoolItem>(out var poolItem))
            poolItem.SetConfig(_config);

        return pooledObject;
    }

    private void ReturnToPool(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
        _pool.Enqueue(pooledObject);
    }

    private void EnsurePool()
    {
        if (_prefab == null || _pool.Count > 0)
            return;

        for (var i = 0; i < InitialPoolSize; i++)
            _pool.Enqueue(CreatePooledInstance());
    }

    private GameObject CreatePooledInstance()
    {
        var instance = Object.Instantiate(_prefab);
        if (instance.TryGetComponent<IPoolItem>(out var poolItem))
            poolItem.SetReturnToPoolAction(() => ReturnToPool(instance));

        instance.SetActive(false);
        return instance;
    }
}
