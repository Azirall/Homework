using UnityEngine;

public static class TransformExtensions
{
    public static Vector2 GetRandomPointInside(this Transform transform)
    {
        return  transform.TransformPoint(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
    }
}