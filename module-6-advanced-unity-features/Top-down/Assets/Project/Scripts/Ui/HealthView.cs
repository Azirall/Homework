using UnityEngine;
using UnityEngine.UI;

public class HealthView :MonoBehaviour, IEventUser
{
    [SerializeField] private Image _healthBar;
    private EventBus _eventBus;

    public void Subscribe(EventBus bus)
    {
        _eventBus = bus;
        _eventBus.OnGameEvent += HandleEvent;
    }

    public void Unsubscribe()
    {
        if (_eventBus == null)
        {
            return;
        }

        _eventBus.OnGameEvent -= HandleEvent;
        _eventBus = null;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is HealthChanged healthChanged)
        {
            float percent = (float)healthChanged.CurrentHealth / healthChanged.MaxHealth;
            _healthBar.fillAmount = percent;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
