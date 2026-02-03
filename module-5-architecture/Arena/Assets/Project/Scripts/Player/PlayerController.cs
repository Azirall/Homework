using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    private InputSystem _inputSystem;

    public void Init(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
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
            
            _rb.AddForce(normalizedInput * _speed,ForceMode2D.Force);
            
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            _rb.rotation = angle - 90f;
        }
    }
}
