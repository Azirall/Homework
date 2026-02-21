
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    
    private Func<Vector3> _getTargetPosition;
    public void Init(Func<Vector3> getPlayerTransform)
    {
        _getTargetPosition = getPlayerTransform;

    }
    private void Update()
    {
        if (_getTargetPosition == null) return;
        
        Vector3 targetPos = _getTargetPosition.Invoke();
        _agent.SetDestination(targetPos);
    }

    private void OnValidate()
    {
        if (_agent == null)
        {
            Debug.LogError($"_navMeshAgent in {_agent} is null");
        }
    }
}
