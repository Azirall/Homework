using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Target : MonoBehaviour
{
    private float _lifetime;
    private TargetStats _stats;

    [Inject]
    public void Construct(TargetStats stats)
    {
        _stats = stats;
    }

    public void Init(float lifetime)
    {
        _lifetime = lifetime;
        Debug.Log("Target created");
        StartCoroutine(nameof(DestroyAfterDelay));
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Target hit!");
        
        _stats.AddDestroyedTarget();
        
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterDelay()
    {
        Debug.Log("Target still alive");
        yield return new WaitForSeconds(_lifetime);
        Debug.Log("Target destroyed");
        Destroy(gameObject);
    }
}
