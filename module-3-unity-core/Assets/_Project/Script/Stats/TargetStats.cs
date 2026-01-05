using Zenject;

public class TargetStats
{
    private int _spawnedTargets = 0;
    private int _destroyedTargets = 0;
    private int _shotsCount = 0;
    
    private TargetStatsView _targetStatsView;
    
    [Inject]
    public void Construct(TargetStatsView view)
    {
        _targetStatsView = view;
    }

    public void AddSpawnedTarget()
    {
        _spawnedTargets++;
         _targetStatsView.ChangeTotalTargetText(_spawnedTargets);
    }

    public void AddDestroyedTarget()
    {
        _destroyedTargets++;
         _targetStatsView.ChangeDestroyedTargetText(_destroyedTargets);
    }

    public void AddShotInCounter()
    {
        _shotsCount++;
        _targetStatsView.ChangeShotCountText(_shotsCount);
    }
}
