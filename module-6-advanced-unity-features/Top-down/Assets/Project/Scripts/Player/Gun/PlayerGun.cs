using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private BulletManager _bulletSpawner;
    private InputSystem _inputSystem;
    private GunLogic _gunLogic; 
    private EventBus _eventBus;
    private WeaponSpread _weaponSpread;
    public void Init(InputSystem inputSystem,EventBus eventBus, GunConfig defaultGun)
    {
        _inputSystem = inputSystem;
        _eventBus = eventBus;
        
        _eventBus.OnGameEvent += HandleEvent;
        
        SetGun(defaultGun);
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
        WeaponMagazine magazine = new(gunData, _eventBus, this);
        _gunLogic = new GunLogic(magazine, gunData);
        _bulletSpawner.InitBullet(gunData.BulletPrefab,gunData.BulletSpeed);
        _weaponSpread = new WeaponSpread(gunData.Spread);
    }

    private void Update()
    {
        if (!_inputSystem.FireButtonPressed) return;
        
        if (_gunLogic.TryShoot())
        {
            Vector3 baseDirection = transform.forward;
            Vector3 spreadDirection = _weaponSpread.ApplySpread(baseDirection);
            _bulletSpawner.SpawnBullet(spreadDirection);
        }
    }
    private void OnValidate()
    {
        if (_bulletSpawner == null)
        {
            Debug.LogError($"BulletSpawner in is null!",this);
        }
    }
}
