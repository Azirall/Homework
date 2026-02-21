using UnityEngine;

public static class SpawnExtensions
{
    public static Vector3 GetSpawnPoint(this BoxCollider collider)
    {
        Bounds bounds = collider.bounds;
        
        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            Vector3 rayOrigin = new Vector3(randomX, bounds.max.y, randomZ);
            
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, bounds.size.y + 5))
            {
                return hit.point;
            }
        }
        Debug.LogWarning($"Не удалось найти поверхность в {collider.name}. Возвращен центр нижней грани.");
        return new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
    }
}