using System;
using UnityEngine;
using Zenject;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Animator _animator;
    
    private InputReader _playerInput;
    private TargetStats _targetStats;
    private float _damage;
    private float _bulletSpeed;
    
    private float _reloadTime;
    private float _currentReloadTime = 0;
    private bool _reloadWarned;
    [Inject]
    public void Construct(InputReader playerInput,TargetStats targetStats)
    {
        _targetStats = targetStats;
        _playerInput = playerInput;
    }

    public void SetNewWeapon(WeaponData weaponData)
    {
        _damage = weaponData.BulletDamage;
        _bulletSpeed = weaponData.BulletSpeed;
        _reloadTime = weaponData.ReloadTime;
    }

    private void Update()
    {
        if (_playerInput == null) return;
        
        if (_currentReloadTime >= 0) _currentReloadTime -= Time.deltaTime;
        if (_currentReloadTime <= 0) _reloadWarned = false;
        
        if (_playerInput.FireButtonPressed && _currentReloadTime <= 0)
        {
            Fire();
        }

        if (_playerInput.FireButtonPressed && _currentReloadTime > 0)
        {
            if (!_reloadWarned)
            {
                Debug.LogWarning("Reloading dont complete");
                _reloadWarned = true;
            }
        }
        
    }

    private void Fire()
    {
        if (_bulletPrefab != null && _bulletSpawnPoint != null)
        {
           Bullet bullet = Instantiate(_bulletPrefab,_bulletSpawnPoint.position,_bulletSpawnPoint.rotation*Quaternion.Euler(90,0,0)).GetComponent<Bullet>();
           
           bullet.Init(20,1,_bulletSpawnPoint.forward.normalized);
           _animator.Play("CameraShake");
           _targetStats.AddShotInCounter();
           _currentReloadTime = _reloadTime;
        }
    }

    private void OnValidate()
    {
        if (_bulletSpawnPoint == null)
        {
            Debug.LogError("Bullet Spawn Point not set");
        }

        if (_bulletPrefab == null)
        {
            Debug.LogError("Missing bullet prefab");
        }
    }
}
