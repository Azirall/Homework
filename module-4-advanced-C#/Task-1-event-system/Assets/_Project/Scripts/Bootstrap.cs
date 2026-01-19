using UnityEngine;

public class Bootstrap : MonoBehaviour
{   
    
    private EventManager _eventManager;
    private void Awake()
    {
        _eventManager = new EventManager();
        
        foreach (IEventManagerConsumer consumer in transform.GetComponentsInChildren<IEventManagerConsumer>())
        {
            consumer.Initialize(_eventManager);
        }
    }
}