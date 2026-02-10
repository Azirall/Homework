using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private InputSystem _inputSystem;
    private float _playerSpeed;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public void Init(InputSystem inputSystem, GameConfig gameConfig)
    {
        _inputSystem = inputSystem;
        _playerSpeed = gameConfig.PlayerSpeed;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
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

    public void ResetToStart()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;

        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}
