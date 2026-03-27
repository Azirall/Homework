using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : IGameState
{
    private readonly IGameOverView _gameOverView;

    public GameOverState(IGameOverView gameOverView)
    {
        _gameOverView = gameOverView;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _gameOverView.RestartRequested += OnRestartRequested;
        _gameOverView.ShowPanel();
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        _gameOverView.RestartRequested -= OnRestartRequested;
        _gameOverView.HidePanel();
    }

    public void Tick()
    {
    }

    private void OnRestartRequested()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
