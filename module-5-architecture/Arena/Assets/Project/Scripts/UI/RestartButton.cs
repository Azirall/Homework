using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void OnClick()
    {
        EventBus.RaiseGameEvent(new GameTriggerEvent(GameTrigger.RestartButtonPressed));
    }
}
