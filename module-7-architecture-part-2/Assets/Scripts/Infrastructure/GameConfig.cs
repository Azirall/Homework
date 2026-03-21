using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private InputSourceKind _inputSource;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    public InputSourceKind InputSource => _inputSource;
    public float MoveSpeed => _moveSpeed;
}
