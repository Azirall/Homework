using System;

public class GameplayState : GameState
{
    private readonly IHealthService _healthService;
    private readonly GameStateMachine _stateMachine;
    private readonly IPauseView _pauseView;

    public GameplayState(IHealthService healthService, GameStateMachine stateMachine, IPauseView pauseView)
    {
        _healthService = healthService;
        _stateMachine = stateMachine;
        _pauseView = pauseView;
    }

    public override void Enter()
    {
        _healthService.HealthChanged += OnHealthChanged;
        _pauseView.PauseRequested += OnPauseRequested;
    }

    public override void Exit()
    {
        _healthService.HealthChanged -= OnHealthChanged;
        _pauseView.PauseRequested -= OnPauseRequested;
    }

    private void OnPauseRequested()
    {
        _stateMachine.ChangeState(typeof(PauseState));
    }

    private void OnHealthChanged(int currentHealth)
    {
        if (currentHealth == 0)
            _stateMachine.ChangeState(typeof(GameOverState));
    }
}
