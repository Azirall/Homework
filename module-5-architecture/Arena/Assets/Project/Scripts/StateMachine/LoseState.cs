public class LoseState : IGameState
{
    public GameTrigger GameTrigger { get; private set; }

    public void Enter()
    {
        EventBus.RaiseGameEvent(new GameTriggerEvent(GameTrigger.PlayerLose));
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}