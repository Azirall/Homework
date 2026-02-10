using System.Collections.Generic;
using UnityEngine;

public class PatrolRocketStrategy : IRocketStrategy
{
    private readonly List<Vector2> _patrolPoints;
    private int _currentPatrolPoint = 0;
    private readonly float _speed;
    private Rigidbody2D _rb;
    
    public PatrolRocketStrategy(List<Vector2> patrolPoints, float moveSpeed)
    {
        _patrolPoints = patrolPoints;
        _speed = moveSpeed;
        
    }

    public void Handle()
    {
        if (_rb == null || _patrolPoints == null || _patrolPoints.Count == 0)
            return;
        
        var pos = (Vector2)_rb.transform.position;
        var target = _patrolPoints[_currentPatrolPoint];
        var to = target - pos;

        if (to.sqrMagnitude < 0.2f * 0.2f)
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPoints.Count;
        
        float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
        _rb.rotation = angle - 90f;
        
        _rb.AddForce(to * _speed);
    }

    public void Init(Rigidbody2D rocketRb)
    {
        _rb = rocketRb;
    }
}
