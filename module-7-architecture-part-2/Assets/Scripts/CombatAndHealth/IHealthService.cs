using System;

public interface IHealthService
{
    event Action<int> HealthChanged;

    void GetDamage(int amount);
    bool CanReceiveHeal();
    void GetHeal(int amount);
    void Death();
}
