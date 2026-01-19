public sealed class GameServices
{
    public EventManager EventManager { get; }
    public AnalyticsPresenter AnalyticsPresenter { get; }
    public GameServices(EventManager eventManager, AnalyticsPresenter analyticsPresenter)
    {
        EventManager = eventManager;
        AnalyticsPresenter = analyticsPresenter;
    }
}
