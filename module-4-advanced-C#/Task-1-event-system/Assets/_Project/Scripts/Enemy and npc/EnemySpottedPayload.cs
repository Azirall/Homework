using UnityEngine;

public struct EnemySpottedPayload : IGameEventPayload
{
    public Vector2 Position { get; }

    public EnemySpottedPayload(Vector2 position)
    {
        Position = position;
    }

    public string ToDisplayString()
    {
        return $"Враг в позиции ({Position.x:0.##}, {Position.y:0.##})";
    }
}
