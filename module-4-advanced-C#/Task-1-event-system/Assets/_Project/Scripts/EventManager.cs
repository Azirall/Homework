using System.Collections.Generic;

public delegate void GameEventHandler(GameEvent e);

public class EventManager
{
    public IReadOnlyList<GameEvent> EventList => _eventsHistory;
    public event GameEventHandler OnGameEvent;
    private readonly List<GameEvent> _eventsHistory = new();
    
    public void TriggerEvent(GameEvent gameEvent)
    {
        _eventsHistory.Add(gameEvent);
        OnGameEvent?.Invoke(gameEvent);
    }
    
}

