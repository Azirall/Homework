using System;

public class PlayerHealth : IDisposable
{
    private int _currentHealth;
    private readonly int _maxHealth;
    private EventBus _eventBus;

    public PlayerHealth(int maxHealth, EventBus eventBus)
    {
        _eventBus = eventBus;
        _currentHealth = maxHealth;
        _maxHealth = maxHealth;
        _eventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is EventTrigger { TriggerType: TriggerType.PlayerDamaged })
        {
            if (_currentHealth > 0)
            {
                _currentHealth--;
                _eventBus.RaiseGameEvent(new HealthChanged(_currentHealth, _maxHealth));
            }
            else
            {
                _eventBus.RaiseGameEvent(new EventTrigger(TriggerType.PlayerKilled));
            }
        }
    }

    public void Dispose()
    {
        if (_eventBus == null)
        {
            return;
        }

        _eventBus.OnGameEvent -= HandleEvent;
        _eventBus = null;
    }
}
