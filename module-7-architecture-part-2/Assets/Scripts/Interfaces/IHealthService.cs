using System;

public interface IHealthService
{
    event Action<int> HealthChanged;

    void GetDamage(int amount);
    void GetHeal(int amount);
    void Death();
}
