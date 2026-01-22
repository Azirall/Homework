using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IGameServicesConsumer
{
    [SerializeField] private Transform _hidePointTransform;
    [SerializeField] private Transform _chestTransform;
    [SerializeField] private float _speed = 1f;

    private readonly Dictionary<GameEventType, Transform> _targets = new();
    private Vector3 _startPos;
    private EventManager _eventManager;
    private Coroutine _coroutine;

    public void Initialize(GameServices services)
    {
        _eventManager = services.EventManager;
        _eventManager.OnGameEvent += HandleEvent;
    }

    private void Awake()
    {
        _startPos = transform.position;
        InitTargetsDictionary();
    }

    private void InitTargetsDictionary()
    {
        _targets[GameEventType.WeatherIsRaining] = _hidePointTransform;
        _targets[GameEventType.BattleStart] = _hidePointTransform;
        _targets[GameEventType.NpcSawChest] = _chestTransform;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        if (!_targets.TryGetValue(gameEvent.EventType, out var target))
            return;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveCoroutine(target));
    }

    IEnumerator MoveCoroutine(Transform target)
    {
        while (Vector2.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (Vector2.Distance(transform.position, _startPos) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (_eventManager != null)
        {
            _eventManager.OnGameEvent -= HandleEvent;
        }
    }
}
