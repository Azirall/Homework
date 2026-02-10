using UnityEngine;

public class InitState : IGameState
{
    private readonly SpawnService _spawnService;
    
    
    public InitState(SpawnService spawnService)
    {
        _spawnService = spawnService;
    }

    public void Enter()
    {
       _spawnService.CreateObjectsInPool();
       Time.timeScale = 0;
    }

    public void Exit()
    {
        
    }
}