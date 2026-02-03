using System;
using UnityEngine;
public class PickupItem : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.RaiseGameEvent(new ItemPickedUp());
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (_collider == null)
        {
            Debug.LogError("Collider in PickupItem is null");
        }
    }
}
