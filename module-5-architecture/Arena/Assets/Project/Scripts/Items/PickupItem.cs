using System;
using UnityEngine;
public class PickupItem : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;
    private Action _returnAction; 
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.RaiseGameEvent(new ItemPicked());
            _returnAction?.Invoke();
        }
    }

    public void Init(Action returnAction)
    {
        _returnAction = returnAction;
    }

    private void OnValidate()
    {
        if (_collider == null)
        {
            Debug.LogError("Collider in PickupItem is null");
        }
    }
}
