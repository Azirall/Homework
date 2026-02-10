using UnityEngine;

public class WinState : IGameState
{
    private readonly SpawnService _spawnService;

    public WinState(SpawnService spawnService)
    {
        _spawnService = spawnService;
    }

    public void Enter()
    {
        _spawnService.StopSpawning();
        Time.timeScale = 0;
        EventBus.RaiseGameEvent(new GameTriggerEvent(GameTrigger.PlayerWin));
    }

    public void Exit()
    {
        
    }
}