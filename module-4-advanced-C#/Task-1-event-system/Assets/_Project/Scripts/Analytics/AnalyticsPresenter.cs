using System.Linq;


public class AnalyticsPresenter 
{
    private EventManager _eventManager;

    public AnalyticsPresenter(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    public string AnalyzeLog()
    {
        var eventList = _eventManager.EventList;

        if (eventList == null || eventList.Count == 0)
        {
            return "Аналитика: событий пока нет.";
        }

        var battleEvents = eventList
            .Where(gameEvent => gameEvent.EventType == GameEventType.BattleStart)
            .Select(gameEvent => gameEvent.ToLogString())
            .ToList();
        var last5 = eventList.OrderByDescending(gameEvent => gameEvent.EventTime).Take(5).Select(gameEvent => gameEvent.ToLogString()).ToList();
        var itemPickedCount = eventList.Count(gameEvent => gameEvent.EventType == GameEventType.ItemPicked);
        var topGroup = eventList.GroupBy(gameEvent => gameEvent.EventType)
            .OrderByDescending(g => g.Count()).First();

        return
            "Аналитика:\n" +
            $"- Боёв начато: {battleEvents.Count}\n" +
            $"- BattleStart events:\n  {string.Join("\n  ", battleEvents)}\n" +
            $"- Предметов подобрано: {itemPickedCount}\n" +
            $"- Самое частое событие: {topGroup.Key} ({topGroup.Count()})\n" +
            $"- Последние 5 событий:\n  {string.Join("\n  ", last5)}";
    }
}

