public class GameTriggerEvent : IGameEvent
{
    public GameTrigger GameTriggerType { get; private set; }

    public GameTriggerEvent(GameTrigger gameTrigger)
    {
        GameTriggerType = gameTrigger;
    }
}