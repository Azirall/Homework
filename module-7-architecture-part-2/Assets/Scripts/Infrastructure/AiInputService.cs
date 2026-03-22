using System;
using UnityEngine;

public class AiInputService : IInputService
{
    private readonly Transform[] _transformPoints;
    private readonly Func<Vector2> _getRigidbodyPosition;
    private int _patrolIndex;
    private bool _goingForward = true;
    private Vector2 _previousToTarget;
    private bool _hasPreviousToTarget;

    public AiInputService(Transform[] patrolPoints, Func<Vector2> getRigidbodyPosition)
    {
        _transformPoints = patrolPoints;
        _getRigidbodyPosition = getRigidbodyPosition;
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 currentPosition = _getRigidbodyPosition();
        if (_transformPoints == null || _transformPoints.Length == 0)
        {
            return Vector2.zero;
        }

        if (_transformPoints.Length == 1)
        {
            return ((Vector2)_transformPoints[0].position - currentPosition).normalized;
        }

        Transform target = _transformPoints[_patrolIndex];
        Vector2 toTarget = (Vector2)target.position - currentPosition;

        if (toTarget == Vector2.zero ||
            (_hasPreviousToTarget && Vector2.Dot(_previousToTarget, toTarget) <= 0f))
        {
            AdvancePatrolIndex();
            target = _transformPoints[_patrolIndex];
            toTarget = (Vector2)target.position - currentPosition;
        }

        _previousToTarget = toTarget;
        _hasPreviousToTarget = true;

        return toTarget == Vector2.zero ? Vector2.zero : toTarget.normalized;
    }

    private void AdvancePatrolIndex()
    {
        int n = _transformPoints.Length;

        if (_goingForward)
        {
            if (_patrolIndex >= n - 1)
            {
                _goingForward = false;
                _patrolIndex = n - 2;
            }
            else
            {
                _patrolIndex++;
            }
        }
        else
        {
            if (_patrolIndex <= 0)
            {
                _goingForward = true;
                _patrolIndex = 1;
            }
            else
            {
                _patrolIndex--;
            }
        }
    }
}