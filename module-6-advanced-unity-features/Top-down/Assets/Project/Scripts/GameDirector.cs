using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : IDisposable
{
    private int _currentScore = 0;
    private EventBus _eventBus;
    private GameStateType _state = (GameStateType)(-1);

    public GameDirector(EventBus eventBus, GameConfig gameConfig)
    {
        _eventBus = eventBus;
        _eventBus.OnGameEvent += HandleEvent;
        EnterState(GameStateType.Start);
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is EventTrigger { TriggerType: TriggerType.EnemyKilled } && _state == GameStateType.Playing)
        {
            _currentScore++;
            _eventBus.RaiseGameEvent(new ScoreChanged(_currentScore.ToString()));
            return;
        }

        if (gameEvent is EventTrigger { TriggerType: TriggerType.PlayerKilled } && _state == GameStateType.Playing)
        {
            EnterState(GameStateType.Lose);
            return;
        }

        if (gameEvent is MenuButtonPressed { ButtonType: MenuButtonType.StartPressed } && _state == GameStateType.Start)
        {
            _currentScore = 0;
            _eventBus.RaiseGameEvent(new ScoreChanged(_currentScore.ToString()));
            EnterState(GameStateType.Playing);
            return;
        }

        if (gameEvent is MenuButtonPressed { ButtonType: MenuButtonType.PausePressed } && _state == GameStateType.Playing)
        {
            EnterState(GameStateType.Pause);
            return;
        }

        if (gameEvent is MenuButtonPressed { ButtonType: MenuButtonType.ContinuePressed } && _state == GameStateType.Pause)
        {
            EnterState(GameStateType.Playing);
            return;
        }

        if (gameEvent is MenuButtonPressed { ButtonType: MenuButtonType.RestartPressed })
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            _currentScore = 0;
            _eventBus.RaiseGameEvent(new ScoreChanged(_currentScore.ToString()));
            EnterState(GameStateType.Playing);
            
        }
    }

    private void EnterState(GameStateType newState)
    {
        Debug.Log("Entered state: " + newState);
        if (_state == newState)
        {
            return;
        }

        _state = newState;
        Time.timeScale = _state == GameStateType.Playing ? 1f : 0f;
        _eventBus.RaiseGameEvent(new GameStateChanged(_state));
    }

    public void Dispose()
    {
        if (_eventBus == null)
        {
            return;
        }

        _eventBus.OnGameEvent -= HandleEvent;
        Time.timeScale = 1f;
        _eventBus = null;
    }
}
