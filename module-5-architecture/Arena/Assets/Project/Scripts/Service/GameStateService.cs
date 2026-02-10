public class GameStateService
{
    private GameStateMachine _stateMachine;
    private int _currentScore;
    private int _targetScore;

    private bool _gameIsPaused;
    public GameStateService(GameConfig gameConfig, GameStateMachine stateMachine)
    {
        _targetScore = gameConfig.TargetScore;
        _stateMachine = stateMachine;
    }

    public void Init()
    {
        EventBus.RaiseGameEvent(new GameStateChanged(GameState.Init));
        EventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is ItemPicked)
        {
            _currentScore++;
            EventBus.RaiseGameEvent(new ScoreChanged(_currentScore));
        }

        if (gameEvent is GameTriggerEvent trigger)
        {
            switch (trigger.GameTriggerType)
            {
                case GameTrigger.PauseButtonPressed:
                    TogglePause();
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
    
}