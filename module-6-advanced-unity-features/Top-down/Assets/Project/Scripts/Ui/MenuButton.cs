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
        Debug.Log($"MenuButton.OnClick: buttonType={_buttonType}, eventBusSet={_eventBus != null}");

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
