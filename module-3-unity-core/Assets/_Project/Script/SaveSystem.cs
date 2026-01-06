using UnityEngine;
public class SaveSystem
{
    public void Save(TargetStats stats)
    {
        if (stats == null)
        {
            return;
        }

        int totalShots = PlayerPrefs.GetInt(nameof(SaveKey.ShotsCount), 0);
        totalShots += stats.ShotsCount;
        int maxDestroyedTargets = PlayerPrefs.GetInt(nameof(SaveKey.MaxDestroyedTargets), 0);
        if (stats.MaxDestroyedTargets > maxDestroyedTargets)
        {
            maxDestroyedTargets = stats.MaxDestroyedTargets;
        }
        
        PlayerPrefs.SetInt(nameof(SaveKey.ShotsCount), totalShots);
        PlayerPrefs.SetInt(nameof(SaveKey.MaxDestroyedTargets), maxDestroyedTargets);
        
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
