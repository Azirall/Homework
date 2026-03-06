using System.Text;
using TMPro;
using UnityEngine;

public class MagazineView : MonoBehaviour, IEventUser
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    private readonly StringBuilder _textBuilder = new StringBuilder(32);
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
            _textBuilder.Clear();
            _textBuilder.Append(ammoChangeEvent.CurrentMagazineSize);
            _textBuilder.Append('/');
            _textBuilder.Append(ammoChangeEvent.MaxMagazineSize);
            _ammoText.text = _textBuilder.ToString();
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
            Debug.LogError("MagazineView: _ammoText is null",this);
        }
    }
}
