using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour, IDamageDealer
{
    [SerializeField] private int _damage = 1;

    public int Damage => _damage;

    private void OnValidate()
    {
        var col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
            Debug.LogWarning($"{nameof(Spikes)}: collider should be a trigger so the player can detect overlap.", this);
    }
}
