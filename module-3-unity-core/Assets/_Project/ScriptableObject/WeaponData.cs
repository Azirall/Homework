
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "NewWeapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _bulletDamage;
    
    public float BulletSpeed => _bulletSpeed;
    public float BulletDamage => _bulletDamage;
    public float ReloadTime => _reloadTime;
}
