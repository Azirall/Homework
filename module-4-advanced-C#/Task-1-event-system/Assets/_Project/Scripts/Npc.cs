using System.Collections;
using UnityEngine;


public class Npc : MonoBehaviour, IEventManagerConsumer
{
    [SerializeField] private Transform _hidePointTransform;
    [SerializeField] private Transform _chestTransform;
    [SerializeField] private float _speed = 1f;

    private Vector3 _startPos;
    private EventManager _eventManager;
    private Coroutine _coroutine;
    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void Awake()
    {
        _eventManager.OnGameEvent += HandleEvent;
        _startPos = transform.position;
    }

    private void HandleEvent(GameEvent gameEvent)
    {

        switch (gameEvent.EventType)
        {
            case GameEventType.WeatherIsRaining:
                StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(MoveCoroutine(_hidePointTransform));
                break;
            
            case GameEventType.BattleStart:
                StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(MoveCoroutine(_hidePointTransform));
                break;

            case GameEventType.NpcSawChest:
                StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(MoveCoroutine(_chestTransform));
                break;
        }
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
        _eventManager.OnGameEvent -= HandleEvent;
    }
}
