using UnityEngine;

[CreateAssetMenu(menuName = "Config/Loot config", fileName = "Loot config")]
public class LootConfig : ScriptableObject
{
  [SerializeField] private GameObject _prefab;
  [SerializeField] private int _lootItemAmount = 5;
  [SerializeField] private float _lootSpawnTime = 2f;
  public int LootItemAmount => _lootItemAmount;
  public float LootSpawnTime => _lootSpawnTime;
  public GameObject Prefab => _prefab;
}