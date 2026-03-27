using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinPickup : MonoBehaviour, ICoin, IPoolItem
{
    
    public int Value { get; private set; } 
    
    private Action _returnToPool;
    

    public void SetReturnToPoolAction(Action returnToPool)
    {
        _returnToPool = returnToPool;
    }

    public void ReturnToPool()
    {
        _returnToPool?.Invoke();
    }

    public void SetConfig(ScriptableObject config)
    {
        if (config is CoinConfig coinConfig)
        {
            Value = coinConfig.Value;
        }
        else
        {
            Value = 1;
        }
    }

    private void OnValidate()
    {
        var col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
            Debug.LogWarning($"{nameof(CoinPickup)}: collider should be a trigger so the player can detect overlap.", this);
    }
}
