using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private WeaponData _currentWeapon;


    private void Start()
    {
        if (_gun != null)
        {
            if (_currentWeapon != null)
            {
                _gun.SetNewWeapon(_currentWeapon);
            }
        }
    }
    private void OnValidate()
    {
        if (_currentWeapon == null)
        {
            Debug.LogError("Weapon not selected");
        }
        if (_gun == null)
        {
            Debug.LogError("Class gun not assigned");
        }
    }
}
