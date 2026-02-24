using UnityEngine;

public class MenuButton : MonoBehaviour, IEventUser
{
    [SerializeField] private MenuButtonType _buttonType;
    private EventBus _eventBus;

    public void Subscribe(EventBus bus)
    {
        _eventBus = bus;
    }

    public void Unsubscribe()
    {
        _eventBus = null;
    }

    public void OnClick()
    {
        if (_eventBus == null)
        {
            return;
        }

        _eventBus.RaiseGameEvent(new MenuButtonPressed(_buttonType));
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
