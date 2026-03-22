using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, GameState> _states = new Dictionary<Type, GameState>();
    private GameState _currentState;

    public GameState CurrentState => _currentState;

    public void RegisterState(Type stateType, GameState state)
    {
        _states[stateType] = state;
    }

    public void ChangeState(Type newStateType)
    {
        if (!_states.TryGetValue(newStateType, out var nextState))
            return;

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    public void Tick()
    {
        _currentState?.Tick();
    }
}
