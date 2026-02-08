using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private InputSystem _inputSystem;
    private float _playerSpeed;

    public void Init(InputSystem inputSystem, GameConfig gameConfig)
    {
        _inputSystem = inputSystem;
        _playerSpeed = gameConfig.PlayerSpeed;
    }

private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector2 input = _inputSystem.MoveInput;

        if (input.sqrMagnitude > 0.01f)
        {
            Vector2 normalizedInput = input.normalized;
            
            _rb.AddForce(normalizedInput * _playerSpeed,ForceMode2D.Force);
            
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            _rb.rotation = angle - 90f;
        }
    }
}
