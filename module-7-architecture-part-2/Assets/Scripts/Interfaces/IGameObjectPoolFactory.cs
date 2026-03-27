using UnityEngine;

public interface IGameObjectPoolFactory
{
    void Init(GameObject prefab, ScriptableObject config);
    GameObject GetFromPool();
}
