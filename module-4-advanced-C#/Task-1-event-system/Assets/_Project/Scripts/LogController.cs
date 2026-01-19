using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour, IGameServicesConsumer
{
    [SerializeField] private GameObject _logItemPrefab;
    [SerializeField] private Transform _logItemContainer;
    [SerializeField] private ScrollRect _scrollRect;
    private EventManager _eventManager;
    private AnalyticsPresenter _analyticsPresenter;
    public void Initialize(GameServices services)
    {
        _analyticsPresenter = services.AnalyticsPresenter;
        _eventManager = services.EventManager;
        _eventManager.OnGameEvent += PrintEvent;
    }

    private void PrintEvent(GameEvent gameEvent)
    {
        string logLine = gameEvent.ToLogString();

        CreateLogItem(logLine);
    }

    public void PrintAnalyzeLog()
    {
        string analyticsLog = _analyticsPresenter.AnalyzeLog();
        CreateLogItem(analyticsLog);
    }

    private void CreateLogItem(string logLine)
    {
        GameObject item = Instantiate(_logItemPrefab, _logItemContainer);
        LogItem logItem = item.GetComponent<LogItem>();

        logItem.SetLogText($"{logLine}\n");

        Canvas.ForceUpdateCanvases();
        _scrollRect.verticalNormalizedPosition = 0f;
    }

    private void OnDestroy()
    {
        if (_eventManager != null)
        {
            _eventManager.OnGameEvent -= PrintEvent;
        }
    }
}
