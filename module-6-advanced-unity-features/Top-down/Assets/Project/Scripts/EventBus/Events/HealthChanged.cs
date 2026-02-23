public class HealthChanged : IGameEvent
{
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public HealthChanged(int currentHealth, int maxHealth)
        {
                CurrentHealth = currentHealth;
                MaxHealth = maxHealth;
        }
}