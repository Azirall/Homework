using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Action _enemyKilled;
    
    public void SetKilledAction(Action killedAction)
    {
        _enemyKilled = killedAction;
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            _enemyKilled?.Invoke();
        }
    }
}