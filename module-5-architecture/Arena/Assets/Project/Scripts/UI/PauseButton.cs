
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnClick()
    {
        EventBus.RaiseGameEvent(new GameTriggerEvent(GameTrigger.PauseButtonPressed));
    }
    
}
