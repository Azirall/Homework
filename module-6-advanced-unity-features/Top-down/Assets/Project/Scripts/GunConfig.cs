using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Player gun", fileName = "New Gun")]
public class GunConfig : ScriptableObject
{
        [SerializeField] private float _reloadTime = 0.2f;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GunType _type;
        public float ReloadTime => _reloadTime;
        public float BulletSpeed => _bulletSpeed;
        public GameObject BulletPrefab => _bulletPrefab;
        public GunType Type => _type;
        private void OnValidate()
        {
                if (_bulletPrefab == null)
                {
                        Debug.LogError($"Bullet prefab in {name} is null");
                }
        }
        
}