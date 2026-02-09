using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rocketRb;
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
        _rocketStrategy.Init(_rocketRb);
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
        EventBus.RaiseGameEvent(new EnemyDestroyed());
        _returnAction();
        
        _returnAction = null;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            EventBus.RaiseGameEvent(new EnemyDestroyed());
            _returnAction();
        }
    }
}