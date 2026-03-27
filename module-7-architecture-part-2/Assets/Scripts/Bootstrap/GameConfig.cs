using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private InputSourceKind _inputSource;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Player")]
    [SerializeField] private int _playerHealth = 100;

    [Header("Spawn")]
    [SerializeField] private float _spawnInterval = 1f;

    public InputSourceKind InputSource => _inputSource;
    public float MoveSpeed => _moveSpeed;
    public int PlayerHealth => _playerHealth;
    public float SpawnInterval => _spawnInterval;
}
