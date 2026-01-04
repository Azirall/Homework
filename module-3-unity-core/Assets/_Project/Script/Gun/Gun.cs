using UnityEngine;
using Zenject;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    
    private PlayerInputReader _playerInput;
    private float _reloadTime = 1f;
    private float _currentReloadTime = 0;
    [Inject]
    public void Construct(PlayerInputReader playerInput)
    {
        _playerInput = playerInput;
    }

    private void Update()
    {
        if (_playerInput != null)
        {
            if (_currentReloadTime >= 0)
            {
                _currentReloadTime -= Time.deltaTime;
                return;
            }
            if (_playerInput.FireButtonPressed)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        if (_bulletPrefab != null && _bulletSpawnPoint != null)
        {
           Bullet bullet = Instantiate(_bulletPrefab,_bulletSpawnPoint.position,_bulletSpawnPoint.rotation*Quaternion.Euler(90,0,0)).GetComponent<Bullet>();
           bullet.Init(20,1,_bulletSpawnPoint.forward.normalized);
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
