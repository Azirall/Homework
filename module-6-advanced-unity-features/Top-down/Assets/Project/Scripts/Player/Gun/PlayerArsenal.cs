using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArsenal : IDisposable
{
    private readonly List<GunConfig> _gunConfigs = new();
    private EventBus _eventBus;
    
    public PlayerArsenal(IReadOnlyList<GunConfig> configs, EventBus eventBus)
    {
        foreach (var config in configs)
        {
            _gunConfigs.Add(config);
        }
        _eventBus = eventBus;
        _eventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is GunChangeButtonPressed buttonEvent)
        {
            GunConfig config = _gunConfigs[buttonEvent.Index-1];
            
            _eventBus.RaiseGameEvent(new GunChanged(config));
        }
    }
    public GunConfig GetDefaultGun()
    {
        return _gunConfigs[0];
    }

    public void Dispose()
    {
        _eventBus.OnGameEvent -= HandleEvent;
        _eventBus = null;
    }
}