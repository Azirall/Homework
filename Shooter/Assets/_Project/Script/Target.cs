using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float _lifetime;
    
    public void Init(float lifetime)
    {
        _lifetime = lifetime;
        Debug.Log("Target created");
        StartCoroutine(nameof(DestroyAfterDelay));
    }

    private IEnumerator DestroyAfterDelay()
    {
        Debug.Log("Target still alive");
        yield return new WaitForSeconds(_lifetime);
        Debug.Log("Target destroyed");
        Destroy(gameObject);
    }
}
