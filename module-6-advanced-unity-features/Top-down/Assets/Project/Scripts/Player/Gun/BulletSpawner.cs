using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
        private GameObject _bulletPrefab;
        private float _bulletSpeed;
    

        public void SpawnBullet(Vector3 direction)
        {
          direction = direction.normalized;

          GameObject bullet = Instantiate(_bulletPrefab,  transform.position, Quaternion.LookRotation(direction));
          
          Rigidbody rb = bullet.GetComponent<Rigidbody>();
          
          rb.velocity = direction * _bulletSpeed;
        }

        public void InitBullet(GameObject prefab, float speed)
        {
             _bulletSpeed = speed;
             _bulletPrefab = prefab;   
        }
        
}