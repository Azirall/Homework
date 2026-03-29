public class NoInputModifier : IGameModifier
{
    private readonly NoInputModifierConfig _config;
    private readonly IMovementBlocker _movementBlocker;
    private readonly ILoggerService _logger;
    private float _phaseElapsed;
    private bool _inDisablePhase;

    public NoInputModifier(NoInputModifierConfig config, IMovementBlocker movementBlocker, ILoggerService logger)
    {
        _config = config;
        _movementBlocker = movementBlocker;
        _logger = logger;
    }

    public void OnEnterGameplay()
    {
        _logger.GameModifierEnabled(nameof(NoInputModifier));
        _phaseElapsed = 0f;
        _inDisablePhase = true;
        if (!_movementBlocker.IsMovementBlocked)
            _movementBlocker.ToggleMovementBlock();
    }

    public void OnExitGameplay()
    {
        if (_movementBlocker.IsMovementBlocked)
            _movementBlocker.ToggleMovementBlock();
    }

    public void Tick(float deltaTime)
    {
        _phaseElapsed += deltaTime;

        if (_inDisablePhase)
        {
            if (_phaseElapsed < _config.DisableDuration)
                return;

            _movementBlocker.ToggleMovementBlock();
            _inDisablePhase = false;
            _phaseElapsed = 0f;
            return;
        }

        if (_phaseElapsed < _config.TimeBetweenReenables)
            return;

        _movementBlocker.ToggleMovementBlock();
        _inDisablePhase = true;
        _phaseElapsed = 0f;
    }
}
