using UnityEngine;

public class LoseState : IGameState
{
    private readonly SpawnService _spawnService;
    public LoseState(SpawnService spawnService)
    {
        _spawnService = spawnService;
    }

    public GameTrigger GameTrigger { get; private set; }

    public void Enter()
    {
        _spawnService.StopSpawning();
        EventBus.RaiseGameEvent(new GameTriggerEvent(GameTrigger.PlayerLose));
        Time.timeScale = 0;
    }

    public void Exit()
    {
    }
}