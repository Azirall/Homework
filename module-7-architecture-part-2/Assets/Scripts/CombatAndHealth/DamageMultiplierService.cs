using System;

public class DamageMultiplierService : IHealthService, IDoubleDamageMultiplier
{
        private readonly IHealthService _healthService;
        public int Multiplier { get; private set; }

        public void SetMultiplier(int multiplier)
        {
                Multiplier = multiplier;
        }

        public DamageMultiplierService(IHealthService healthService)
        { 
                _healthService = healthService;
                Multiplier = 1;
        }

        public event Action<int> HealthChanged;
        public void GetDamage(int amount)
        {
                _healthService.GetDamage(amount * Multiplier);
        }

        public bool CanReceiveHeal()
        {
            return _healthService.CanReceiveHeal();
        }

        public void GetHeal(int amount)
        {
                _healthService.GetHeal(amount);
        }

        public void Death()
        {
                _healthService.Death();
        }

        
}