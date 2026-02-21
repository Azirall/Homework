using System.Collections.Generic;
using UnityEngine;

public class UiContext : MonoBehaviour
{
    [SerializeField] private Transform _uiRoot;
    private List<IEventConsumer> _eventConsumers = new();
    
    public IReadOnlyList<IEventConsumer> EventConsumers => _eventConsumers;
    private void OnValidate()
    {
        _eventConsumers.AddRange(_uiRoot.GetComponentsInChildren<IEventConsumer>());
    }
}