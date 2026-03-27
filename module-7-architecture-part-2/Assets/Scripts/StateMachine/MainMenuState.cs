using UnityEngine;

public class MainMenuState : IGameState
{
    private readonly IMainMenuView _mainMenuView;
    private readonly GameController _gameController;

    public MainMenuState(IMainMenuView mainMenuView, GameController gameController)
    {
        _mainMenuView = mainMenuView;
        _gameController = gameController;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _mainMenuView.StartRequested += OnStartRequested;
        _mainMenuView.ShowPanel();
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        _mainMenuView.StartRequested -= OnStartRequested;
        _mainMenuView.HidePanel();
    }

    public void Tick()
    {
    }

    private void OnStartRequested()
    {
        _gameController.StartGameplay();
    }
}
