using System;
using UnityEngine;

public class UiInitializer : IDisposable
{
    private readonly UiContext _uiContext;
    private readonly GunContext _gunContext;
    private readonly EventBus _eventBus;

    public UiInitializer(UiContext uiContext, GunContext gunContext, EventBus eventBus)
    {
        _uiContext = uiContext;
        _gunContext = gunContext;
        _eventBus = eventBus;
    }

    public void Init()
    {
        if (_uiContext == null || _gunContext == null || _eventBus == null)
        {
            Debug.LogError("UiInitializer: one of required dependencies is null");
            return;
        }

        _uiContext.GunPanelController.Init(_gunContext);

        foreach (IEventUser consumer in _uiContext.EventConsumers)
        {
            if (consumer == null)
            {
                continue;
            }

            consumer.Subscribe(_eventBus);
        }
    }

    public void Dispose()
    {
        if (_uiContext == null)
        {
            return;
        }

        foreach (IEventUser consumer in _uiContext.EventConsumers)
        {
            if (consumer == null)
            {
                continue;
            }

            consumer.Unsubscribe();
        }
    }
}

