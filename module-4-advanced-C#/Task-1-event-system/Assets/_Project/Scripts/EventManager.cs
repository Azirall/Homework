using System.Collections.Generic;

public delegate void GameEventHandler(GameEvent e);

public class EventManager
{
    public event GameEventHandler OnGameEvent;
    private List<GameEvent> _eventsHistory = new List<GameEvent>();
    public void TriggerEvent(GameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
        _eventsHistory.Add(gameEvent);
    }
    
}

