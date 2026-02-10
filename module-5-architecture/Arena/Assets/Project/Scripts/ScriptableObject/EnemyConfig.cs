using UnityEngine;

[CreateAssetMenu(menuName = "Config/Enemy config", fileName = "Enemy config")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private EnemyStrategyType _type;
    [SerializeField] private float _enemySpeed = 4f;
    [SerializeField] private float _lifeTime = 6f;

    public EnemyStrategyType StrategyType => _type;
    public GameObject Prefab => _enemyPrefab;
    public float MoveSpeed => _enemySpeed;
    public float LifeTime => _lifeTime;
}
