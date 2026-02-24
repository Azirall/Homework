using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    [SerializeField] private float _playerSpeed = 5f;
    public float PlayerSpeed => _playerSpeed;

    [Header("GameSettings")]
    [SerializeField] private int _scoreForWin = 10;
    [SerializeField] private int _timeToNewWave = 20;
    [SerializeField] private float _spawnDelay = 1;
    [SerializeField] private int _enemiesInWave = 5;
    
    
    public int EnemiesInWave => _enemiesInWave;
    public int ScoreForWin => _scoreForWin;
    public int TimeToNewWave => _timeToNewWave;
    public float SpawnDelay => _spawnDelay;
    
} 