
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerGun _playerGun;

    [Header("System")]
    [SerializeField] private InputSystem _inputSystem;

    public PlayerGun PlayerGun => _playerGun;
    public PlayerController PlayerController => _playerController;
    public InputSystem InputSystem => _inputSystem;
}