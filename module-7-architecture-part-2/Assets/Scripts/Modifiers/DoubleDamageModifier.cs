public class DoubleDamageModifier : IGameModifier
{
    private readonly DoubleDamageModifierConfig _config;
    private readonly IDoubleDamageMultiplier _doubleDamageMultiplier;

    public DoubleDamageModifier(DoubleDamageModifierConfig config, IDoubleDamageMultiplier doubleDamageMultiplier)
    {
        _config = config;
        _doubleDamageMultiplier = doubleDamageMultiplier;
    }

    public void OnEnterGameplay()
    {
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