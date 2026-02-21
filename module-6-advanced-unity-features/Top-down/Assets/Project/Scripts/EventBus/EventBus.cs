using System;

public class EventBus
{
    public event Action<IGameEvent> OnGameEvent;

    public void RaiseGameEvent(IGameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }
}