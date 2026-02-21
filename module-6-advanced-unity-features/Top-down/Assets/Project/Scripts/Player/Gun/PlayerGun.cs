using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private GunConfig _gunData;
    
    private InputSystem _inputSystem;
    private GunLogic _gunLogic; 

    public void Init(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
        
        _gunLogic = new GunLogic(_gunData);
        
        _bulletSpawner.SetBulletPrefab(_gunData.BulletPrefab);
    }

    private void Update()
    {
        if (!_inputSystem.FireButtonPressed) return;
        
        if (_gunLogic.CanFire(Time.time))
        {
            _bulletSpawner.SpawnBullet(_gunData.BulletSpeed);
            
            _gunLogic.RegisterShot(Time.time);
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
