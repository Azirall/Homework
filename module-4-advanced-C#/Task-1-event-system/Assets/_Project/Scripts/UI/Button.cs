using UnityEngine;

public class Button : MonoBehaviour, IGameServicesConsumer
{
    [SerializeField] private GameEventType _eventType;
    [SerializeField] private string _gameEventDescription;
    [SerializeField] private bool _eventTargetIsNpc;
    private EventManager _eventManager;

    public void Initialize(GameServices services)
    {
        _eventManager = services.EventManager;
    }

    public void OnClick()
    {
        GameEvent gameEvent = new(_eventType, _gameEventDescription);
        _eventManager.TriggerEvent(gameEvent);
    }
}
