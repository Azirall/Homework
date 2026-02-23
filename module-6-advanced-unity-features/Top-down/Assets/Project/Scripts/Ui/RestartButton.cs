using System;
using UnityEngine;

public class RestartButton : MonoBehaviour , IEventUser
{
    private EventBus _eventBus;
    public void Subscribe(EventBus bus)
    {
        _eventBus = bus;
    }

    public void OnClick()
    {
        _eventBus.RaiseGameEvent(new EventTrigger(TriggerType.PauseButtonPressed));
    }
}
