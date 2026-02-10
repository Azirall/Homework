
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<GameState, IGameState> _states = new();
    private IGameState _currentState;

    public GameStateMachine(SpawnService spawnService)
    {
         InitState initState = new InitState(spawnService);
         PlayingState playingState = new PlayingState(spawnService);
         PausedState pausedState = new PausedState(spawnService);
         WinState winState = new WinState(spawnService);
         LoseState loseState = new LoseState(spawnService);
         
         _states.Add(GameState.Init, initState);
         _states.Add(GameState.Paused, pausedState);
         _states.Add(GameState.Playing, playingState);
         _states.Add(GameState.Win, winState);
         _states.Add(GameState.Lose, loseState);
         
         SetState(GameState.Init);
    }
    public void SetState(GameState state)
    {
        _currentState?.Exit();
        _currentState = _states[state];
        _currentState?.Enter();
    }
}
