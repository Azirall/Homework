public class GameManager
{
    private readonly GameStateMachine _stateMachine;

    public GameManager(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
}
