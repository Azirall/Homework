using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    
    private InputSystem _inputSystem;
    private GunConfig _gunData;
    private float _nextFireTime = 0;
    
    public void Init(InputSystem inputSystem, GunConfig gunData)
    {
        _inputSystem = inputSystem;
        
        SetGun(gunData);
        
        if (_inputSystem == null || gunData == null)
        {
            Debug.LogError($"Initialization failed on {name}! Missing dependencies.");
            
            enabled = false; 
        }

    }

    public void SetGun(GunConfig gunData)
    {
        _gunData = gunData;
        _bulletSpawner.SetBulletPrefab(_gunData.BulletPrefab);
    }

    private void Update()
    {
        if (_inputSystem.FireButtonPressed)
        {
            TryFire();
        }
    }

    private void TryFire()
    {
        if (Time.time >= _nextFireTime)
        {
            _bulletSpawner.SpawnBullet(_gunData.BulletSpeed);
            _nextFireTime = Time.time + _gunData.ReloadTime;
        }
    }



    private void OnValidate()
    {
        if (_bulletSpawner == null)
        {
            Debug.LogError($"BulletSpawner in {name} is null!");
        }
    }
}
