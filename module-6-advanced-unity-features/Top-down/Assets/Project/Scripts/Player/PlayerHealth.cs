
public class PlayerHealth 
{
    private int _currentHealth;
    private int _maxHealth;
    private readonly EventBus _eventBus;
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
}