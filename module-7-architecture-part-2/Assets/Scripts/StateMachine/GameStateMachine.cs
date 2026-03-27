using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
    private readonly ILoggerService _logger;
    private IGameState _currentState;

    public IGameState CurrentState => _currentState;

    public GameStateMachine(ILoggerService logger)
    {
        _logger = logger;
    }

    public void RegisterState(Type stateType, IGameState state)
    {
        _states[stateType] = state;
    }

    public void ChangeState(Type newStateType)
    {
        if (!_states.TryGetValue(newStateType, out var nextState))
            return;

        var previousStateName = _currentState?.GetType().Name ?? "None";
        var nextStateName = nextState.GetType().Name;
        _logger.StateTransition(previousStateName, nextStateName);

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    public void Tick()
    {
        _currentState?.Tick();
    }
}
