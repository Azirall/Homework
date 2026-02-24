using System.Collections.Generic;
using UnityEngine;

public class UiContext : MonoBehaviour
{
    [SerializeField] private List<Transform> _uiRoots = new List<Transform>();
    [SerializeField] private GunPanelController _gunPanelController;
    private List<IEventUser> _eventConsumers = new();

    public IReadOnlyList<IEventUser> EventConsumers => _eventConsumers;
    public GunPanelController GunPanelController => _gunPanelController;
    private void Awake()
    {
        CollectEventConsumers();
    }

    private void OnValidate()
    {
        CollectEventConsumers();
    }

    private void CollectEventConsumers()
    {
        _eventConsumers.Clear();

        foreach (Transform uiRoot in _uiRoots)
        {
            if (uiRoot == null)
            {
                continue;
            }

            _eventConsumers.AddRange(uiRoot.GetComponentsInChildren<IEventUser>(true));
        }
    }
}
