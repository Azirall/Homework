using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    private Action _returnAction;
    private float _moveSpeed;
    private IRocketStrategy _rocketStrategy;
    
    public void Init(Action returnAction)
    {
        _returnAction = returnAction;
    }
    public void SetStrategy(IRocketStrategy strategy)
    {
        _rocketStrategy = strategy;
    }

    private void FixedUpdate()
    {
        _rocketStrategy?.Handle();
    }

    public void DieAfterSecond(float seconds)
    {
        StartCoroutine(DieCoroutine(seconds));
    }

    IEnumerator DieCoroutine(float second)
    {
        yield return new WaitForSeconds(second);
        _returnAction?.Invoke();
        _returnAction = null;
    }
}