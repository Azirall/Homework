using System;

public class GameEvent
{
    public GameEventType EventType { get; private set; }
    public DateTime EventTime { get; private set; }
    public string Description { get; private set; }
    
    public GameEvent(GameEventType eventType, string description)
    {
        EventType = eventType;
        EventTime = DateTime.Now;
        Description = description;

    }
}

