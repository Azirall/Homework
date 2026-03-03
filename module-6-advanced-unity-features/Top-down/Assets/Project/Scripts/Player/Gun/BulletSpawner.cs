using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
        private GameObject _bulletPrefab;
        private float _bulletSpeed;
    

        public void SpawnBullet()
        {
          GameObject bullet = Instantiate(_bulletPrefab,  transform.position, transform.rotation);
          
          Rigidbody rb = bullet.GetComponent<Rigidbody>();
          
          rb.velocity = transform.forward * _bulletSpeed;
        }

        public void InitBullet(GameObject prefab, float speed)
        {
             _bulletSpeed = speed;
             _bulletPrefab = prefab;   
        }
        
}