using System;
using TMPro;
using UnityEngine;

public class MagazineView : MonoBehaviour, IEventUser
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    private EventBus _eventBus;
    public void Subscribe(EventBus bus)
    {
        _eventBus = bus;
        _eventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is AmmoChangeEvent ammoChangeEvent)
        {
            _ammoText.text = $"{ammoChangeEvent.CurrentMagazineSize}/{ammoChangeEvent.MaxMagazineSize}";
        }
    }
    public void Unsubscribe()
    {
        _eventBus.OnGameEvent -= HandleEvent;
    }

    private void OnValidate()
    {
        if (_ammoText == null)
        {
            Debug.LogError("MagazineView: _ammoTmp is null");
        }
    }
}
