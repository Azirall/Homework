using UnityEngine;

public class EnemyVision : MonoBehaviour, IGameServicesConsumer
{
    private EventManager _eventManager;

    public void Initialize(GameServices services)
    {
        _eventManager = services.EventManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            GameEvent gameEvent = new(GameEventType.BattleStart, "нпс прячется от угрозы");
            _eventManager.TriggerEvent(gameEvent);
        }
    }
}
