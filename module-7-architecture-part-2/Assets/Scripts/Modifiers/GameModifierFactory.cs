using System;
using System.Collections.Generic;

public class GameModifierFactory
{
    private readonly List<GameModifierConfig> _configs;
    private readonly IHealthService _healthService;
    private readonly IMovementBlocker _movementBlocker;
    private readonly IDoubleDamageMultiplier _doubleDamageMultiplier;
    private readonly ILoggerService _logger;
    private readonly List<IGameModifier> _modifiers;
    
    public GameModifierFactory(List<GameModifierConfig> configs, IHealthService healthService, IMovementBlocker movementBlocker, IDoubleDamageMultiplier damageMultiplier, ILoggerService logger)
    {
        _configs = configs;
        _healthService = healthService;
        _movementBlocker = movementBlocker;
        _doubleDamageMultiplier = damageMultiplier;
        _logger = logger;
        
        _modifiers = new List<IGameModifier>();

        foreach (var config in _configs)
        {
            if (config == null)
                continue;

            switch (config)
            {
                case RegenModifierConfig regenConfig:
                    _modifiers.Add(new RegenModifier(regenConfig, _healthService, _logger));
                    break;
                case DoubleDamageModifierConfig doubleDamageConfig:
                    _modifiers.Add(new DoubleDamageModifier(doubleDamageConfig, _doubleDamageMultiplier, _logger));
                    break;
                case NoInputModifierConfig noInputConfig:
                    _modifiers.Add(new NoInputModifier(noInputConfig, _movementBlocker, _logger));
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported modifier config type: {config.GetType().Name}");
            }
        }
    }

    public IReadOnlyList<IGameModifier> Modifiers => _modifiers;
}
