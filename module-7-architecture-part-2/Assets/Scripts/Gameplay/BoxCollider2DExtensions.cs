using UnityEngine;

public static class BoxCollider2DExtensions
{
    public static Vector2 GetRandomPointInZone(this BoxCollider2D boxCollider)
    {
        var bounds = boxCollider.bounds;
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
