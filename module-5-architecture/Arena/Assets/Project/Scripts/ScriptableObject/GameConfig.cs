using UnityEngine;

[CreateAssetMenu(menuName = "Config/Game config", fileName = "Game config")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _enemyRespawnTime = 3f;
    [SerializeField] private int _maxEnemy = 5;
    [SerializeField] private int _targetScore = 10;
    
    public float PlayerSpeed => _playerSpeed;
    public float EnemyRespawnTime => _enemyRespawnTime;
    public int MaxEnemy => _maxEnemy;
    public int TargetScore => _targetScore;
}