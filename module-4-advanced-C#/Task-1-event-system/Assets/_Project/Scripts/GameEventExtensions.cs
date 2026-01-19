using UnityEngine;

public class GameEventExtensions
{
    public string BuildLog(GameEvent gameEvent)
    {
        int minutes = Mathf.FloorToInt(gameEvent.EventTime / 60f);
        float seconds = gameEvent.EventTime % 60f;
        string eventName = GetEventName(gameEvent.EventType);
        return $"[{minutes:00}:{seconds:00.0}] {eventName}:  {gameEvent.Description}";
    }

    private string GetEventName(GameEventType eventType)
    {
        switch (eventType)
        {
            case GameEventType.BattleStart:
                return "Начало битвы";
            case GameEventType.NpcSawChest:
                return "НПС увидел сундук";
            case GameEventType.ItemPickUp:
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
