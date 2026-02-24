using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputSystem : MonoBehaviour
{
    public Vector2 MoveInput {get; private set; }
    public Vector2 MousePosition { get; private set; }
    
    public bool FireButtonPressed { get; private set; }
    private EventBus _eventBus;

    public void Init(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();        
    }

    public void OnMousePosition(InputValue value)
    {
        MousePosition = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        FireButtonPressed = value.isPressed;
    }

    public void OnCancel(InputValue value)
    {
        if (!value.isPressed || _eventBus == null)
        {
            return;
        }

        _eventBus.RaiseGameEvent(new MenuButtonPressed(MenuButtonType.PausePressed));
    }

    public void OnQuickSlot(InputAction.CallbackContext context)
    {
        Key key = ((KeyControl)context.control).keyCode;
        
        int slotIndex = (int)key - (int)Key.Digit1 + 1;
        
        
    }

    private void OnDestroy()
    {
        _eventBus = null;
    }
}
