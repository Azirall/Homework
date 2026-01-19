using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Transform _root;
    private EventManager _eventManager;
    private GameEventExtensions _gameEventExtensions;
    private void Awake()
    {
        _eventManager = new EventManager();
        _gameEventExtensions = new GameEventExtensions();
        
        foreach (IEventManagerConsumer consumer in _root.GetComponentsInChildren<IEventManagerConsumer>())
        {
            consumer.Initialize(_eventManager);
        }

        LogView logView = _root.GetComponentInChildren<LogView>();
        if (logView != null)
        {
            logView.Initialize(_gameEventExtensions);
        }
    }
}
