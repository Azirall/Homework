using UnityEngine;
using Zenject;

public class SaveSystem
{
    private TargetStats _stats;

    [Inject]
    private void Construct(TargetStats stats)
    {
        _stats = stats;
    }


    public void Save()
    {
        int totalShots = PlayerPrefs.GetInt(nameof(SaveKey.ShotsCount), 0);
        totalShots += _stats.ShotsCount;
        
        PlayerPrefs.SetInt(nameof(SaveKey.ShotsCount), totalShots);
        PlayerPrefs.SetInt(nameof(SaveKey.MaxDestroyedTargets),_stats.MaxDestroyedTargets);
        
        PlayerPrefs.Save();
        Debug.Log("Data saved");
    }

    public int Load(SaveKey key)
    {
        return PlayerPrefs.GetInt(key.ToString(), 0);
    }
}

public enum SaveKey
{
    MaxDestroyedTargets,
    ShotsCount
}