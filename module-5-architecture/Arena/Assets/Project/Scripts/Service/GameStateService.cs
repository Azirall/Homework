using UnityEngine;

public class GameStateService
{
    private int _currentScore;
    private int _targetScore;

    public GameStateService(GameConfig gameConfig)
    {
        _targetScore = gameConfig.TargetScore;
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
    }

    public void StartGame()
    {
        EventBus.RaiseGameEvent(new GameStateChanged(GameState.Playing));
    }

    public void PauseGame()
    {
        EventBus.RaiseGameEvent(new GameStateChanged(GameState.Paused));
    }
    
}