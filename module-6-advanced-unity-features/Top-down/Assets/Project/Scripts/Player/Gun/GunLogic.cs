public class GunLogic
{
    private readonly GunConfig _config;
    private float _nextFireTime;

    public GunLogic(GunConfig config)
    {
        _config = config;
    }
    public bool CanFire(float currentTime)
    {
        return currentTime >= _nextFireTime;
    }

    public void RegisterShot(float currentTime)
    {
        _nextFireTime = currentTime + _config.ReloadTime;
    }   
}