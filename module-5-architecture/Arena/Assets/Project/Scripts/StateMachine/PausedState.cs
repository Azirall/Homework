using UnityEngine;

public class PausedState : IGameState
{
    private SpawnService _spawnService;

    public PausedState(SpawnService spawnService)
    {
        _spawnService = spawnService;
    }

    public void Enter()
    {
        _spawnService.StopSpawning();
        Time.timeScale = 0;
    }

    public void Exit()
    {
        
    }
}