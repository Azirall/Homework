using UnityEngine;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
        [SerializeField] private Transform _bulletContainer;
        
        private GameObject _bulletPrefab;
        private float _bulletSpeed;

        private readonly Queue<Bullet> _bulletPool = new();
        private const int _initialPoolSize = 10;
        private bool _isPrewarmed = false;

        private void Start()
        {
          if (_bulletPrefab != null)
          {
            PrewarmPoolIfNeeded();
            
          }
        }

        private void OnValidate()
        {
          if (_bulletContainer == null)
          {
            Debug.LogError($"Bullet container in {name} is null!", this);
          }
        }


        public void SpawnBullet(Vector3 direction)
        {
          direction = direction.normalized;

          PrewarmPoolIfNeeded();

          Bullet bullet = GetBulletFromPool();
          bullet.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(direction));
          bullet.gameObject.SetActive(true);

          Rigidbody rb = bullet.Rigidbody;
          if (rb != null)
          {
            rb.velocity = direction * _bulletSpeed;
          }
        }

        public void InitBullet(GameObject prefab, float speed)
        {
             _bulletSpeed = speed;
             
             if (_bulletPrefab != prefab)
             {
               _bulletPrefab = prefab;
               ClearPool();
               _isPrewarmed = false;
             }

             if (_bulletPrefab != null)
             {
               PrewarmPoolIfNeeded();
             }
        }

        private void DespawnBullet(Bullet bullet)
        {
          if (bullet == null) return;

          Rigidbody rb = bullet.Rigidbody;
          if (rb != null)
          {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
          }

          bullet.gameObject.SetActive(false);
          _bulletPool.Enqueue(bullet);
        }

        private Bullet GetBulletFromPool()
        {
          if (_bulletPool.Count == 0)
          {
            CreateAndEnqueueBullet();
          }

          return _bulletPool.Dequeue();
        }

        private void PrewarmPoolIfNeeded()
        {
          if (_bulletPrefab == null) return;
          if (_isPrewarmed) return;

          while (_bulletPool.Count < _initialPoolSize)
          {
            CreateAndEnqueueBullet();
          }

          _isPrewarmed = true;
        }

        private void CreateAndEnqueueBullet()
        {
          GameObject bulletObject = Instantiate(_bulletPrefab, _bulletContainer);
          bulletObject.transform.SetParent(_bulletContainer, true);
          Bullet bullet = bulletObject.GetComponent<Bullet>();
          bullet.Init(DespawnBullet);
          bulletObject.SetActive(false);
          _bulletPool.Enqueue(bullet);
        }

        private void ClearPool()
        {
          while (_bulletPool.Count > 0)
          {
            Bullet bullet = _bulletPool.Dequeue();
            if (bullet != null)
            {
              Destroy(bullet.gameObject);
            }
          }
        }
}