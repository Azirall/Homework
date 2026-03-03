using UnityEngine;

[CreateAssetMenu(menuName = "Player gun", fileName = "New Gun")]
public class GunConfig : ScriptableObject
{
        [SerializeField] private float _spread = 1f;
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private float _reloadTime = 0.2f;
        [SerializeField] private int _magazineSize = 11;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private GameObject _bulletPrefab;
        
        public float Spread => Spread;
        public float FireRate => _fireRate;
        public int MagazineSize => _magazineSize;
        public float ReloadTime => _reloadTime;
        public float BulletSpeed => _bulletSpeed;
        public GameObject BulletPrefab => _bulletPrefab;
        private void OnValidate()
        {
                if (_bulletPrefab == null)
                {
                        Debug.LogError($"Bullet prefab in {name} is null");
                }
        }
        
}