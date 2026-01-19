using System;
using UnityEngine;

public class Chest : MonoBehaviour, IEventManagerConsumer
{
    private EventManager _eventManager;
    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameEvent gameEvent = new GameEvent(GameEventType.ItemPickUp, "предмет был взят");
        _eventManager.TriggerEvent(gameEvent);
    }
}
