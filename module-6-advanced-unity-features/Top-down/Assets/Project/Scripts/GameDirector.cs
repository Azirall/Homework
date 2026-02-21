public class GameDirector
{
    private int _currentScore = 0;
    private EventBus _eventBus;
    public GameDirector(EventBus eventBus, GameConfig gameConfig)
    {
        _eventBus = eventBus;
        _eventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is EventTrigger { TriggerType: TriggerType.EnemyKilled })
        {
            _currentScore++;
            _eventBus.RaiseGameEvent(new ScoreChanged(_currentScore.ToString()));
        }
    }

}