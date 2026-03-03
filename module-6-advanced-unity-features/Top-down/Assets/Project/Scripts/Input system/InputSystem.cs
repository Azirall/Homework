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

    public void OnQuickSlot(InputValue value)
    {
        var keyboard = Keyboard.current;
        
        for (int i = (int)Key.Digit1; i <= (int)Key.Digit9; i++)
        {
            if (keyboard[(Key)i].wasPressedThisFrame)
            {
                int slotIndex = i - (int)Key.Digit1 + 1;
            
                _eventBus.RaiseGameEvent(new GunChangeButtonPressed(slotIndex));
            }
        }
    }

    private void OnDestroy()
    {
        _eventBus = null;
    }
}
