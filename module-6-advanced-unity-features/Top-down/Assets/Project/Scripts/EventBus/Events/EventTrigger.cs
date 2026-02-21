public class EventTrigger : IGameEvent
{
        public TriggerType TriggerType { get; private set; }

        public EventTrigger(TriggerType triggerType)
        {
                TriggerType = triggerType;
        }
}