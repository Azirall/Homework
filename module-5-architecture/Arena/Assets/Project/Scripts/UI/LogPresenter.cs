using System.Collections.Generic;
using UnityEngine;

public class LogPresenter : MonoBehaviour
{
    [SerializeField] private LogView _logView;
    [SerializeField] private int _logsToDisplay = 3;
    
    private EventLogger _eventLogger;
    private bool _isVisible;

    public void Init(EventLogger eventLogger)
    {
        _eventLogger = eventLogger;
        RefreshLogs();
    }

    private void Start()
    {
        EventBus.OnGameEvent += HandleEvent;
    }

    private void OnDestroy()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }
    
    private void HandleEvent(IGameEvent gameEvent)
    {
        RefreshLogs();

        if (gameEvent is LogVisibilityChanged)
        {
            _isVisible = !_isVisible; 
            if (_logView != null) _logView.SetVisible(_isVisible);
        }
    }

    private void RefreshLogs()
    {
        if (_eventLogger == null || _logView == null) return;

        var logs = _eventLogger.Logs;
        var takeCount = Mathf.Min(_logsToDisplay, logs.Count);
        var startIndex = logs.Count - takeCount;

        var lastLogs = new List<string>(takeCount);
        for (var i = startIndex; i < logs.Count; i++)
            lastLogs.Add(logs[i]);

        _logView.SetLogs(lastLogs);
    }
}
