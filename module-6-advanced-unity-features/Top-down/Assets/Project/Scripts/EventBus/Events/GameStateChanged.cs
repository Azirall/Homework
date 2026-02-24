public class GameStateChanged : IGameEvent
{
    public GameStateType State { get; private set; }

    public GameStateChanged(GameStateType state)
    {
        State = state;
    }
}
