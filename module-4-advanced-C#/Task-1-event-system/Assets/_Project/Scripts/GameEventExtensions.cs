public static class GameEventExtensions
{
    public static string ToLogString(this GameEvent gameEvent)
    {
        string timeStamp = gameEvent.EventTime.ToString("HH:mm:ss");
        string eventName = GetEventName(gameEvent.EventType);
        return $"[{timeStamp}] {eventName}: {gameEvent.Description}";
    }

    private static string GetEventName(GameEventType eventType)
    {
        switch (eventType)
        {
            case GameEventType.BattleStart:
                return "Начало битвы";
            case GameEventType.NpcSawChest:
                return "НПС увидел сундук";
            case GameEventType.ItemPicked:
                return "Подбор предмета";
            case GameEventType.WeatherIsRaining:
                return "Начался дождь";
            case GameEventType.OnEnemyActiveStateChanged:
                return "Смена активности врагов";
            default:
                return "Неизвестное событие";
        }
    }
}
