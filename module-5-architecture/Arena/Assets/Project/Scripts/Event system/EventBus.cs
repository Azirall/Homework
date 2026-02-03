using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<IGameEvent> OnGameEvent;

    public static void RaiseGameEvent(IGameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }
}