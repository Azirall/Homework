using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private EnemyMove _mover;
    
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _attackDelay = 1f;
    
    private System.Func<Vector3> _getTargetPosition;
    private float _nextAttackTime;
    private EventBus _eventBus;
    
    public void Init(System.Func<Vector3> getTargetPosition, EventBus eventBus)
    {
        _getTargetPosition = getTargetPosition;
        _eventBus = eventBus;
    }

    private void Update()
    {
        if (_getTargetPosition == null) return;

        Vector3 targetPos = _getTargetPosition.Invoke();
        float sqrDistance = (targetPos - transform.position).sqrMagnitude;

        if (sqrDistance > _attackDistance * _attackDistance)
        {
            _mover.MoveTo(targetPos);
        }
        else
        {
            _mover.Stop();

            if (Time.time >= _nextAttackTime)
            {
                _eventBus.RaiseGameEvent(new EventTrigger(TriggerType.PlayerDamaged));
                _nextAttackTime = Time.time + _attackDelay;
            }
        }
    }
}