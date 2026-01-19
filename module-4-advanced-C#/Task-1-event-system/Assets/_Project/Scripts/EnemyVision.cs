
using UnityEngine;


public class EnemyVision : MonoBehaviour, IEventManagerConsumer
{
    private EventManager _eventManager;

    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            GameEvent gameEvent = new(GameEventType.BattleStart,"НПС прячется");
            _eventManager.TriggerEvent(gameEvent);
        }
    }
    
}
