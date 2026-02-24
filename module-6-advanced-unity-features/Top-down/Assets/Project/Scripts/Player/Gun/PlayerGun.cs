using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    private InputSystem _inputSystem;
    private GunConfig _gunData;
    private GunLogic _gunLogic; 
    private EventBus _eventBus;
    public void Init(InputSystem inputSystem,EventBus eventBus, GunConfig gunData)
    {
        _inputSystem = inputSystem;
        _eventBus = eventBus;
        
        _eventBus.OnGameEvent += HandleEvent;
        
        _gunData = gunData;
        SetGun(gunData);
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is GunChanged gunChangedEvent)
        {
            SetGun(gunChangedEvent.GunConfig);
        }
    }

    private void SetGun(GunConfig gunData)
    {
        _gunData = gunData;
        _gunLogic = new GunLogic(gunData);
        _bulletSpawner.SetBulletPrefab(gunData.BulletPrefab);
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
