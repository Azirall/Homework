using JetBrains.Annotations;
using UnityEngine;

public class GameEvent
{
    public GameEventType EventType { get; private set; }
    public float EventTime { get;private set; }
    public string Description { get; private set; }
    
    public object Payload { get; private set; }
    public GameEvent(GameEventType eventType, string description, [CanBeNull] object payload)
    {
        EventType = eventType;
        EventTime = Time.time;
        Description = description;
        Payload = payload;
    }
}

