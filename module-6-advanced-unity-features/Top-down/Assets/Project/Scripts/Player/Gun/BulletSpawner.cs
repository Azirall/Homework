using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
        private GameObject _bulletPrefab;
        
        public void SpawnBullet(float speed)
        {
          GameObject bullet = Instantiate(_bulletPrefab,  transform.position, transform.rotation);
          
          Rigidbody rb = bullet.GetComponent<Rigidbody>();
          
          rb.velocity = transform.forward * speed;
        }

        public void SetBulletPrefab(GameObject prefab)
        {
             _bulletPrefab = prefab;   
        }
        
}