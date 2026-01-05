using System;

public class TargetStats
{
    private int _spawnedTargets = 0;
    private int _destroyedTargets = 0;
    private int _shotsCount = 0;

    public int MaxDestroyedTargets {get; private set; }

    public event Action<int> SpawnedTargetsChanged;
    public event Action<int> DestroyedTargetsChanged;
    public event Action<int> ShotsCountChanged;

    public int SpawnedTargets => _spawnedTargets;
    public int DestroyedTargets => _destroyedTargets;
    public int ShotsCount => _shotsCount;

    public void AddSpawnedTarget()
    {
        _spawnedTargets++;
        SpawnedTargetsChanged?.Invoke(_spawnedTargets);
    }

    public void AddDestroyedTarget()
    {
        _destroyedTargets++;
        CheckForMaxDestroyedTargets();
        DestroyedTargetsChanged?.Invoke(_destroyedTargets);
    }

    public void AddShotInCounter()
    {
        _shotsCount++;
        ShotsCountChanged?.Invoke(_shotsCount);
    }

    private void CheckForMaxDestroyedTargets()
    {
        if (_destroyedTargets > MaxDestroyedTargets)
        {
            MaxDestroyedTargets = _destroyedTargets;
        }
    }
}
