using System.Collections.Generic;
using UnityEngine;

public delegate void GameEventHandler(GameEvent e);

public class EventManager
{
    public event GameEventHandler OnGameEvent;
    
    private List<GameEvent> _eventsHistory = new();
    
    public void TriggerEvent(GameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
        
        Debug.Log(gameEvent.Description);
        
        _eventsHistory.Add(gameEvent);
    }
    
}
