using System;
using UnityEngine;
using System.Threading;

public class Npc : MonoBehaviour, IEventManagerConsumer
{
    [SerializeField] private Transform _hidePointTransform;
    [SerializeField] private Transform _chestTransform;
    [SerializeField] private float _speed = 1f;

    private Vector3 _startPos;
    private Rigidbody2D _rb;
    private EventManager _eventManager;

    private CancellationTokenSource _moveCts;

    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _eventManager.OnGameEvent += HandleEvent;
        _startPos = transform.position;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        switch (gameEvent.EventType)
        {
            case GameEventType.WeatherIsRaining:
                StartMove(NpcMoveDestination.Hide);
                break;
            
            case GameEventType.BattleStart:
                StartMove(NpcMoveDestination.Hide);
                break;

            case GameEventType.NpcMoveToChest:
                StartMove(NpcMoveDestination.Chest);
                break;
        }
    }

    private void StartMove(NpcMoveDestination destination)
    {

    }

    private void OnDestroy()
    {
        _eventManager.OnGameEvent -= HandleEvent;
    }
}
