using System;
using UnityEngine;

public class Chest : MonoBehaviour, IGameServicesConsumer
{
    private EventManager _eventManager;

    public void Initialize(GameServices services)
    {
        _eventManager = services.EventManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameEvent gameEvent = new GameEvent(GameEventType.ItemPicked, "предмет был подобран");
        _eventManager.TriggerEvent(gameEvent);
    }
}
