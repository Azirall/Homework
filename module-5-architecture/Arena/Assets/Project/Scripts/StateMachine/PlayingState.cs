using UnityEngine;

public class PlayingState : IGameState
{
    private SpawnService _spawnService;

    public PlayingState(SpawnService spawnService)
    {
        _spawnService = spawnService;
    }

    public void Enter()
    {
        _spawnService.StartSpawning();
        Time.timeScale = 1;
    }

    public void Exit()
    {
        
    }
}