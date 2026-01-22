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
            var payload = new EnemySpottedPayload(transform.position);
            GameEvent gameEvent = new(GameEventType.BattleStart, "нпс прячется от угрозы", payload);
            _eventManager.TriggerEvent(gameEvent);
        }
    }
}
