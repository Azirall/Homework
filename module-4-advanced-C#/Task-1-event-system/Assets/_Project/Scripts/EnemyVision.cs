
using UnityEngine;
using Zenject;

public class EnemyVision : MonoBehaviour
{
    private EventManager _eventManager;

    [Inject]
    public void Construct(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            GameEvent gameEvent = new(GameEventType.BattleStart,"Противник обнаружил нпс в точке: ",other.transform.position);
            _eventManager.TriggerEvent(gameEvent);
        }
    }
}