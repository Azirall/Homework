using UnityEngine;

public class PauseState : IGameState
{
    private readonly IPauseView _pauseView;
    private readonly GameController _gameController;

    public PauseState(IPauseView pauseView, GameController gameController)
    {
        _pauseView = pauseView;
        _gameController = gameController;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _pauseView.ResumeRequested += OnResumeRequested;
        _pauseView.ToMenuRequested += OnToMenuRequested;
        _pauseView.ShowPanel();
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        _pauseView.ResumeRequested -= OnResumeRequested;
        _pauseView.ToMenuRequested -= OnToMenuRequested;
        _pauseView.HidePanel();
    }

    public void Tick()
    {
    }

    private void OnResumeRequested()
    {
        _gameController.ResumeGameplay();
    }

    private void OnToMenuRequested()
    {
        _gameController.OpenMainMenu();
    }
}
