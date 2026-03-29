public class GameController
{
    private readonly GameStateMachine _stateMachine;

    public GameController(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void StartGame()
    {
        ChangeState<MainMenuState>();
    }

    public void Tick()
    {
        _stateMachine.Tick();
    }

    public void StartGameplay()
    {
        ChangeState<GameplayState>();
    }

    public void PauseGame()
    {
        ChangeState<PauseState>();
    }

    public void ResumeGameplay()
    {
        ChangeState<GameplayState>();
    }

    public void OpenMainMenu()
    {
        ChangeState<MainMenuState>();
    }

    public void OpenGameOver()
    {
        ChangeState<GameOverState>();
    }

    private void ChangeState<TState>()
    {
        _stateMachine.ChangeState(typeof(TState));
    }
}
