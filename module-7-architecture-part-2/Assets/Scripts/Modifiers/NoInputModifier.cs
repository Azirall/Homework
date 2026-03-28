public class NoInputModifier : IGameModifier
{
    private readonly NoInputModifierConfig _config;
    private readonly IMovementBlocker _movementBlocker;
    private float _phaseElapsed;
    private bool _inDisablePhase;

    public NoInputModifier(NoInputModifierConfig config, IMovementBlocker movementBlocker)
    {
        _config = config;
        _movementBlocker = movementBlocker;
    }

    public void OnEnterGameplay()
    {
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
