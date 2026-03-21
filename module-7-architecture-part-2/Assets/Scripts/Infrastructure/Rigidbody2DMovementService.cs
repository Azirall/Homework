using UnityEngine;

public class Rigidbody2DMovementService : IMovementService
{
    private readonly Rigidbody2D _rigidbody2D;

    public Rigidbody2DMovementService(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }

    public void Move(Vector2 direction)
    {
        _rigidbody2D.velocity = direction;
    }
}
