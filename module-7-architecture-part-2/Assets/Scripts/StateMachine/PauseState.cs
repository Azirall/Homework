using UnityEngine;

public class PauseState : GameState
{
    private readonly IPauseView _pauseView;
    private readonly GameStateMachine _stateMachine;

    public PauseState(IPauseView pauseView, GameStateMachine stateMachine)
    {
        _pauseView = pauseView;
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        Time.timeScale = 0f;
        _pauseView.ResumeRequested += OnResumeRequested;
        _pauseView.ToMenuRequested += OnToMenuRequested;
        _pauseView.ShowPanel();
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
        _pauseView.ResumeRequested -= OnResumeRequested;
        _pauseView.ToMenuRequested -= OnToMenuRequested;
        _pauseView.HidePanel();
    }

    private void OnResumeRequested()
    {
        _stateMachine.ChangeState(typeof(GameplayState));
    }

    private void OnToMenuRequested()
    {
        _stateMachine.ChangeState(typeof(MainMenuState));
    }
}
