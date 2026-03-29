using UnityEngine;

public class RegenModifier : IGameModifier
{
    private readonly RegenModifierConfig _config;
    private readonly IHealthService _healthService;
    private readonly ILoggerService _logger;
    private float _currentTime;

    public RegenModifier(RegenModifierConfig config, IHealthService healthService, ILoggerService logger)
    {
        _config = config;
        _healthService = healthService;
        _logger = logger;
    }

    public void OnEnterGameplay()
    {
        _logger.GameModifierEnabled(nameof(RegenModifier));
        _currentTime = Time.time;
    }

    public void OnExitGameplay()
    {
    }

    public void Tick(float deltaTime)
    {
        if (Time.time >= _currentTime + _config.TimeBetweenRegenTicks)
        {
            if (_healthService.CanReceiveHeal())
                _healthService.GetHeal(_config.RegenValue);

            _currentTime = Time.time;
        }
    }
}
