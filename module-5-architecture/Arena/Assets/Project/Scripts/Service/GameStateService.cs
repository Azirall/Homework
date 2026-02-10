public class GameStateService
{
    private readonly GameStateMachine _stateMachine;
    private readonly SpawnService _spawnService;
    private readonly PlayerController _playerController;

    private int _currentHealth;
    private int _maxHealth;
    
    private int _currentScore;
    private readonly int _targetScore;

    private bool _gameIsPaused;
    public GameStateService(GameConfig gameConfig, GameStateMachine stateMachine, SpawnService spawnService, PlayerController playerController)
    {
        _maxHealth = gameConfig.PlayerHealth;
        _currentHealth = _maxHealth;
        
        _targetScore = gameConfig.TargetScore;
        _stateMachine = stateMachine;
        _spawnService = spawnService;
        _playerController = playerController;
    }

    public void Init()
    {
        EventBus.RaiseGameEvent(new GameStateChanged(GameState.Init));
        EventBus.OnGameEvent += HandleEvent;
    }

    public void Dispose()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is ItemPicked)
        {
            _currentScore++;
            EventBus.RaiseGameEvent(new ScoreChanged(_currentScore));

            if (_currentScore >= _targetScore)
            {
                EventBus.RaiseGameEvent(new GameStateChanged(GameState.Win));
                _stateMachine.SetState(GameState.Win);
            }
        }

        if (gameEvent is PlayerDamaged)
        {
            _currentHealth--;
            if (_currentHealth <= 0)
            {
                EventBus.RaiseGameEvent(new GameStateChanged(GameState.Lose));
                _stateMachine.SetState(GameState.Lose);
                return;
            }
        }

        if (gameEvent is GameTriggerEvent trigger)
        {
            switch (trigger.GameTriggerType)
            {
                case GameTrigger.PauseButtonPressed:
                    TogglePause();
                    break;
                case GameTrigger.RestartButtonPressed:
                    RestartGame();
                    break;
            }
        }
    }

    private void TogglePause()
    {
        if (_gameIsPaused)
        {
            _stateMachine.SetState(GameState.Playing);
            EventBus.RaiseGameEvent(new GameStateChanged(GameState.Playing));
            _gameIsPaused = false;
        }
        else
        {
            _stateMachine.SetState(GameState.Paused);
            EventBus.RaiseGameEvent(new GameStateChanged(GameState.Paused));
            _gameIsPaused = true;
        }
    }

    public void StartGame()
    {
        _stateMachine.SetState(GameState.Playing);
    }

    private void RestartGame()
    {
        _spawnService.ResetAllSpawnedObjects();

        _currentHealth = _maxHealth;
        _currentScore = 0;
        _gameIsPaused = false;

        _playerController.ResetToStart();

        EventBus.RaiseGameEvent(new ScoreChanged(_currentScore));

        _stateMachine.SetState(GameState.Playing);
        EventBus.RaiseGameEvent(new GameStateChanged(GameState.Playing));
    }
    
}
