using System;

public class GameEvent
{
    public GameEventType EventType { get; private set; }
    public DateTime EventTime { get; private set; }
    public string Description { get; private set; }
    public IGameEventPayload Payload { get; private set; }
    
    public GameEvent(GameEventType eventType, string description, IGameEventPayload payload = null)
    {
        EventType = eventType;
        EventTime = DateTime.Now;
        Description = description;
        Payload = payload;

    }
}

