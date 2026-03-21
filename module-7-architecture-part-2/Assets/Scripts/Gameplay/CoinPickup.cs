using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinPickup : MonoBehaviour, ICoin
{
    [SerializeField] private int _value = 1;

    public int Value => _value;

    private void OnValidate()
    {
        var col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
            Debug.LogWarning($"{nameof(CoinPickup)}: collider should be a trigger so the player can detect overlap.", this);
    }
}
