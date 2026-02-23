using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    public void MoveTo(Vector3 destination)
    {
        _agent.isStopped = false;
        _agent.SetDestination(destination);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    private void OnValidate()
    {
        if (_agent == null)
        {
            Debug.LogError($"_navMeshAgent in {_agent} is null");
        }
    }
}
