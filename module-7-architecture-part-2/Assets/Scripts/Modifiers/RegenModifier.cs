using UnityEngine;

public class RegenModifier : IGameModifier
{
    private readonly RegenModifierConfig _config;
    private readonly IHealthService _healthService;
    private float _currentTime;

    public RegenModifier(RegenModifierConfig config, IHealthService healthService)
    {
        _config = config;
        _healthService = healthService;
    }

    public void OnEnterGameplay()
    {
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
