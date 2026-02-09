
using UnityEngine;

public class LogPresenter : MonoBehaviour
{
    [SerializeField] private LogView _logView;
    
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
        _logView.SetLogs(_eventLogger.Logs);
    }
}
