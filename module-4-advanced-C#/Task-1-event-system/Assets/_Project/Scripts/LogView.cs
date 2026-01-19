
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogView : MonoBehaviour, IEventManagerConsumer
{
    [SerializeField] private GameObject _logItemPrefab;
    [SerializeField] private Transform _logItemContainer;
    [SerializeField] private ScrollRect _scrollRect;
    private EventManager _eventManager;
    private GameEventExtensions _gameEventExtensions;

    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
        _eventManager.OnGameEvent += PrintEvent;
    }

    public void Initialize(GameEventExtensions gameEventExtensions)
    {
        _gameEventExtensions = gameEventExtensions;
    }

    private void PrintEvent(GameEvent gameEvent)
    {
        string logLine = _gameEventExtensions.BuildLog(gameEvent);
        GameObject item =  Instantiate(_logItemPrefab, _logItemContainer);
        LogItem logItem = item.GetComponent<LogItem>();
        logItem.SetLogText($"{logLine}\n");
        Canvas.ForceUpdateCanvases();
        _scrollRect.verticalNormalizedPosition = 0f;
    }

    private void OnDestroy()
    {
        _eventManager.OnGameEvent -= PrintEvent;
    }
}
