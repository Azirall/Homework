using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArsenal : IDisposable
{
    private Dictionary<GunType, GunConfig> _gunConfigs = new();
    private EventBus _eventBus;
    
    public PlayerArsenal(IReadOnlyList<GunConfig> configs, EventBus eventBus)
    {
        foreach (var config in configs)
        {
            GunType type = config.Type;
            _gunConfigs.Add(type, config);
        }
        _eventBus = eventBus;
        _eventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is GunChangeButtonPressed buttonEvent)
        {
            Debug.Log(buttonEvent.Index);
        }
    }
    public GunConfig GetDefaultGun()
    {
        return TryGetGun(GunType.Pistol);
    }
    private GunConfig TryGetGun(GunType type)
    {
        _gunConfigs.TryGetValue(type, out var config);
        return config;
    }

    public void Dispose()
    {
        _eventBus.OnGameEvent -= HandleEvent;
        _eventBus = null;
    }
}