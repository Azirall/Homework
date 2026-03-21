public class HealthService : IHealthService
{
    private readonly int _maxHealth;
    private int _currentHealth;

    public HealthService(int initialHealth)
    {
        _maxHealth = initialHealth;
        _currentHealth = initialHealth;
    }

    public void GetDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth < 0)
            _currentHealth = 0;
    }

    public void GetHeal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void Death()
    {
        _currentHealth = 0;
    }
}
