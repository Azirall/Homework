using System;
using UnityEngine;

public interface IPoolItem
{
    void SetReturnToPoolAction(Action returnToPool);
    void ReturnToPool();

    void SetConfig(ScriptableObject config);
}
