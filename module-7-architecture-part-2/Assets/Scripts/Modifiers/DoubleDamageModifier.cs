public class DoubleDamageModifier : IGameModifier
{
    private readonly DoubleDamageModifierConfig _config;
    private readonly IHealthService _healthService;

    public DoubleDamageModifier(DoubleDamageModifierConfig config, IHealthService healthService)
    {
        _config = config;
        _healthService = healthService;
    }

    public void OnEnterGameplay()
    {
    }

    public void OnExitGameplay()
    {
    }

    public void Tick(float deltaTime)
    {
    }
}