using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : IGameState
{
    private readonly IGameOverView _gameOverView;
    private readonly ILoggerService _logger;

    public GameOverState(IGameOverView gameOverView, ILoggerService logger)
    {
        _gameOverView = gameOverView;
        _logger = logger;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _gameOverView.RestartRequested += OnRestartRequested;
        _gameOverView.ToMenuRequested += OnToMenuRequested;
        _gameOverView.ShowPanel();
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        _gameOverView.RestartRequested -= OnRestartRequested;
        _gameOverView.ToMenuRequested -= OnToMenuRequested;
        _gameOverView.HidePanel();
    }

    public void Tick()
    {
    }

    private void OnRestartRequested()
    {
        Time.timeScale = 1f;
        _logger.Restart();
        GameInstaller.StartGameplayAfterSceneLoad = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnToMenuRequested()
    {
        Time.timeScale = 1f;
        GameInstaller.StartGameplayAfterSceneLoad = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
