using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
        private GameObject _bulletPrefab;
        
        public void SpawnBullet(float speed)
        {
          GameObject bullet = Instantiate(_bulletPrefab, transform.position, _bulletPrefab.transform.rotation);
          
          Rigidbody rb = bullet.GetComponent<Rigidbody>();
          
          rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        public void SetBulletPrefab(GameObject prefab)
        {
             _bulletPrefab = prefab;   
        }
        
}