using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Transform _root;

    private void Awake()
    {
        EventManager eventManager = new EventManager();
        AnalyticsPresenter analyticsPresenter = new AnalyticsPresenter(eventManager);
        GameServices services = new GameServices(eventManager,analyticsPresenter);

        foreach (IGameServicesConsumer consumer in _root.GetComponentsInChildren<IGameServicesConsumer>(true))
        {
            consumer.Initialize(services);
        }
    }
}
