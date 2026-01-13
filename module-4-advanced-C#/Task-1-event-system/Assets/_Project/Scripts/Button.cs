using UnityEngine;
using Zenject;

public class Button : MonoBehaviour
{
    [SerializeField] private GameEventType _eventType;
    [SerializeField] private string _gameEventDescription;
    private EventManager _eventManager;
    
    [Inject]
    public void Construct(EventManager eventManager)
    {
        _eventManager = eventManager;
    }
    
    public void OnClick()
    {
        GameEvent gameEvent = new(_eventType, _gameEventDescription, null);
        _eventManager.TriggerEvent(gameEvent);
    }
}
