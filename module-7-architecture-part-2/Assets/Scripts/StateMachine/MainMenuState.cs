using UnityEngine;

public class MainMenuState : GameState
{
    private readonly IMainMenuView _mainMenuView;
    private readonly GameStateMachine _stateMachine;

    public MainMenuState(IMainMenuView mainMenuView, GameStateMachine stateMachine)
    {
        _mainMenuView = mainMenuView;
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        Time.timeScale = 0f;
        _mainMenuView.StartRequested += OnStartRequested;
        _mainMenuView.ShowPanel();
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
        _mainMenuView.StartRequested -= OnStartRequested;
        _mainMenuView.HidePanel();
    }

    private void OnStartRequested()
    {
        _stateMachine.ChangeState(typeof(GameplayState));
    }
}
