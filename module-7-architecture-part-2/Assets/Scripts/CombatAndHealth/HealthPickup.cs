using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour, IHealDealer
{
    [SerializeField] private int _healAmount = 1;

    public int HealAmount => _healAmount;

    private void OnValidate()
    {
        var col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
            Debug.LogWarning($"{nameof(HealthPickup)}: collider should be a trigger so the player can detect overlap.", this);
    }
}
