using System;

public class HealthService : IHealthService
{
    private readonly int _maxHealth;
    private readonly ILoggerService _logger;
    private int _currentHealth;

    public event Action<int> HealthChanged;

    public HealthService(int initialHealth, ILoggerService logger)
    {
        _maxHealth = initialHealth;
        _currentHealth = initialHealth;
        _logger = logger;
    }

    public void GetDamage(int amount)
    {
        _logger.DamageReceived(amount);
        SetCurrentHealth(_currentHealth - amount);
    }

    public void GetHeal(int amount)
    {
        SetCurrentHealth(_currentHealth + amount);
    }

    public void Death()
    {
        SetCurrentHealth(0);
    }

    private void SetCurrentHealth(int value)
    {
        if (value < 0)
            value = 0;
        else if (value > _maxHealth)
            value = _maxHealth;

        if (_currentHealth == value)
            return;

        _currentHealth = value;
        HealthChanged?.Invoke(_currentHealth);
    }
}
