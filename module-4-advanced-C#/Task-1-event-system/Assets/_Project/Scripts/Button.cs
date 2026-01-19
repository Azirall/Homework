using UnityEngine;

public class Button : MonoBehaviour,IEventManagerConsumer
{
    [SerializeField] private GameEventType _eventType;
    [SerializeField] private string _gameEventDescription;
    private EventManager _eventManager;
    
    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
    }
    
    public void OnClick()
    {
        GameEvent gameEvent = new(_eventType, _gameEventDescription);
        _eventManager.TriggerEvent(gameEvent);
    }
}
