using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private int _playerMaxHealth = 10;
    public float PlayerSpeed => _playerSpeed;
    public int PlayerMaxHealth => _playerMaxHealth;

    [Header("GameSettings")]
    [SerializeField] private int _wavesCount = 2;
    [SerializeField] private float _spawnDelay = 1;
    [SerializeField] private int _enemiesInWave = 5;
    
    
    public int WavesCount => _wavesCount;
    public int EnemiesInWave => _enemiesInWave;
    public float SpawnDelay => _spawnDelay;
    
} 