using UnityEngine;

public class GunLogic
{ 
    private readonly float _fireRateCooldown;
    private float _lastFireTime;
    private readonly WeaponMagazine  _magazine;
    
    private float _nextFireTime;
    public GunLogic(WeaponMagazine magazine, GunConfig config)
    {
        _magazine = magazine;
        _fireRateCooldown = config.FireRate;
    }
    public bool TryShoot()
    {
        if (Time.time - _lastFireTime < _fireRateCooldown) return false;

        if (!_magazine.TryConsumeAmmo())
        {
            _magazine.Reload();
            return false;
        }
        
        _lastFireTime = Time.time;
        return true;
    }
}