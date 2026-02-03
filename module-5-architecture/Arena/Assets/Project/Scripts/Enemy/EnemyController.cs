using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Action _returnAction;
    private float _moveSpeed;
    public void Init(Action returnAction,float speed)
    {
        _returnAction = returnAction;
        _moveSpeed = speed;
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