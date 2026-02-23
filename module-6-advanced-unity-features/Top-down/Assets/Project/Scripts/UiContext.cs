using System.Collections.Generic;
using UnityEngine;

public class UiContext : MonoBehaviour
{
    [SerializeField] private List<Transform> _uiRoots = new List<Transform>();
    private List<IEventUser> _eventConsumers = new();
    
    public IReadOnlyList<IEventUser> EventConsumers => _eventConsumers;
    private void OnValidate()
    {
        foreach (var uiRoot in _uiRoots)
        {
            _eventConsumers.AddRange(uiRoot.GetComponentsInChildren<IEventUser>());
        }
    }
}