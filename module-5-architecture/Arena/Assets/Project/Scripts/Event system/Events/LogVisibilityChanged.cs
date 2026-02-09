public class LogVisibilityChanged : IGameEvent
{
    public void ChangeVisibility()
    {
        EventBus.RaiseGameEvent(new LogVisibilityChanged());
    }
}