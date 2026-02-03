public class GameStateChanged : IGameEvent
{
    public GameState GameState { get; private set; }

    public GameStateChanged(GameState gameState)
    {
        GameState = gameState;
    }
}