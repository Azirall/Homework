using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _gravity = 1f;
    public float PlayerSpeed => _playerSpeed;
    public float Gravity => _gravity;
}