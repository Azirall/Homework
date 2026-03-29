public class DoubleDamageModifier : IGameModifier
{
    private readonly DoubleDamageModifierConfig _config;
    private readonly IDoubleDamageMultiplier _doubleDamageMultiplier;
    private readonly ILoggerService _logger;

    public DoubleDamageModifier(DoubleDamageModifierConfig config, IDoubleDamageMultiplier doubleDamageMultiplier, ILoggerService logger)
    {
        _config = config;
        _doubleDamageMultiplier = doubleDamageMultiplier;
        _logger = logger;
    }

    public void OnEnterGameplay()
    {
        _logger.GameModifierEnabled(nameof(DoubleDamageModifier));
        _doubleDamageMultiplier.SetMultiplier(_config.DamageMultiplier);
    }

    public void OnExitGameplay()
    {
        _doubleDamageMultiplier.SetMultiplier(1);
    }

    public void Tick(float deltaTime)
    {
    }
}