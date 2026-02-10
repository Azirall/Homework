using UnityEngine;

public class WinState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 0;
        EventBus.RaiseGameEvent(new GameTriggerEvent(GameTrigger.PlayerWin));
    }

    public void Exit()
    {
        
    }
}