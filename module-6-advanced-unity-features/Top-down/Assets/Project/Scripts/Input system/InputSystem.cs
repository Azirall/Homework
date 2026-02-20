using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public Vector2 MoveInput {get; private set; }
    public Vector2 MousePosition { get; private set; }
    
    public bool FireButtonPressed { get; private set; }
    
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
    
}
