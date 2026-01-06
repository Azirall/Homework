using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Target : MonoBehaviour
{
    private float _lifetime;
    private TargetStats _stats;
    private Animator _animator;
    [Inject]
    public void Construct(TargetStats stats)
    {
        _stats = stats;
    }

    public void Init(float lifetime)
    {
        _lifetime = lifetime;
        _animator = GetComponent<Animator>();
        Debug.Log("Target created");
        StartCoroutine(nameof(DestroyAfterDelay));
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Target hit!");
    
        _stats.AddDestroyedTarget();
        
        _animator.SetBool("IsDead", true);
        Destroy(gameObject,0.5f);
    }
   



    private IEnumerator DestroyAfterDelay()
    {
        Debug.Log("Target still alive");
        yield return new WaitForSeconds(_lifetime);
        Debug.Log("Target destroyed");
        Destroy(gameObject);
    }
}
