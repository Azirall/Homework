public class GameplayState : IGameState
{
    private readonly IHealthService _healthService;
    private readonly GameController _gameController;
    private readonly IPauseView _pauseView;
    private readonly ISpawnController _spawnController;

    public GameplayState(IHealthService healthService, GameController gameController, IPauseView pauseView, ISpawnController spawnController)
    {
        _healthService = healthService;
        _gameController = gameController;
        _pauseView = pauseView;
        _spawnController = spawnController;
    }

    public void Enter()
    {
        _healthService.HealthChanged += OnHealthChanged;
        _pauseView.PauseRequested += OnPauseRequested;
    }

    public void Exit()
    {
        _healthService.HealthChanged -= OnHealthChanged;
        _pauseView.PauseRequested -= OnPauseRequested;
    }

    public void Tick()
    {
        _spawnController.Tick();
    }

    private void OnPauseRequested()
    {
        _gameController.PauseGame();
    }

    private void OnHealthChanged(int currentHealth)
    {
        if (currentHealth == 0)
            _gameController.OpenGameOver();
    }
}
