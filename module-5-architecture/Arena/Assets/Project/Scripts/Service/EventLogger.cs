using System;
using System.Collections.Generic;

public class EventLogger
{
    private readonly List<string> _logList = new();

    public IReadOnlyList<string> Logs => _logList;

    public void Init()
    {
        EventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case EnemySpawned:
                Log("EnemySpawned", "Enemy has spawned.");
                break;
            case PlayerDamaged:
                Log("PlayerDamaged", "Player received 1 damage.");
                break;
            case ItemPicked:
                Log("ItemPicked", "Item has been picked up.");
                break;
            case GameStateChanged gameStateChanged:
                Log("GameStateChanged", $"Game state changed to {gameStateChanged.GameState}.");
                break;
        }
    }

    private void Log(string eventName, string description)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _logList.Add($"{timestamp} : {eventName} - {description}");
    }
}