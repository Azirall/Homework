using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using System.Threading;

public class Npc : MonoBehaviour
{
    [SerializeField] private Transform _hidePointTransform;
    [SerializeField] private Transform _chestTransform;
    [SerializeField] private float _speed = 1f;

    private Vector3 _startPos;
    private Rigidbody2D _rb;
    private EventManager _eventManager;

    private CancellationTokenSource _moveCts;

    [Inject]
    public void Construct(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }

    private void OnEnable()
    {
        if (_eventManager != null)
            _eventManager.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        if (_eventManager != null)
            _eventManager.OnGameEvent -= HandleEvent;

        CancelMove();
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
        CancelMove();
        _moveCts = new CancellationTokenSource();

        RunMove(destination, _moveCts.Token).Forget();
    }

    private void CancelMove()
    {
        if (_moveCts == null) return;

        _moveCts.Cancel();
        _moveCts.Dispose();
        _moveCts = null;
    }

    private async UniTask RunMove(NpcMoveDestination destination, CancellationToken ct)
    {
        if (destination == NpcMoveDestination.Chest)
        {
            await MoveTo(_chestTransform.position, ct);

            var gameEvent = new GameEvent(GameEventType.ItemPickUp, "Нпс забрал предмет в координатах: ", _chestTransform);
            _eventManager.TriggerEvent(gameEvent);

            await MoveTo(_startPos, ct);
        }
        else 
        {
            await MoveTo(_hidePointTransform.position, ct);
            await UniTask.Delay(2000, cancellationToken: ct);
            await MoveTo(_startPos, ct);
        }
    }

    private async UniTask MoveTo(Vector3 targetPos, CancellationToken ct)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            ct.ThrowIfCancellationRequested();

            Vector2 next = Vector2.MoveTowards(_rb.position, (Vector2)targetPos, _speed * Time.fixedDeltaTime);

            _rb.MovePosition(next);
            await UniTask.WaitForFixedUpdate(ct);
        }
    }
}
