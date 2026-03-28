using System;
using System.Collections.Generic;

public class GameModifierFactory
{
    private readonly List<GameModifierConfig> _configs;
    private readonly IHealthService _healthService;
    private readonly IMovementBlocker _movementBlocker;
    private readonly List<IGameModifier> _modifiers;

    public GameModifierFactory(List<GameModifierConfig> configs, IHealthService healthService, IMovementBlocker movementBlocker)
    {
        _configs = configs ?? new List<GameModifierConfig>();
        _healthService = healthService;
        _movementBlocker = movementBlocker;
        _modifiers = new List<IGameModifier>();

        foreach (var config in _configs)
        {
            if (config == null)
                continue;

            switch (config)
            {
                case RegenModifierConfig regenConfig:
                    _modifiers.Add(new RegenModifier(regenConfig, _healthService));
                    break;
                case DoubleDamageModifierConfig doubleDamageConfig:
                    _modifiers.Add(new DoubleDamageModifier(doubleDamageConfig, _healthService));
                    break;
                case NoInputModifierConfig noInputConfig:
                    _modifiers.Add(new NoInputModifier(noInputConfig, _movementBlocker));
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported modifier config type: {config.GetType().Name}");
            }
        }
    }

    public IReadOnlyList<IGameModifier> Modifiers => _modifiers;
}
