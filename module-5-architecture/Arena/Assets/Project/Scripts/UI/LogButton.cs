using UnityEngine;

public class LogButton : MonoBehaviour
{
    public void OnClick()
    {
        EventBus.RaiseGameEvent(new LogVisibilityChanged());
    }
}
