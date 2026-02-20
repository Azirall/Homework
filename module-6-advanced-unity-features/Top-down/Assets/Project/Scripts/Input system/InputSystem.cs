using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public Vector2 MoveInput {get; private set; }
    public Vector2 LookInput { get; private set; }
    
    public bool FireButtonPressed { get; private set; }
    
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();        
    }

    public void OnLook(InputValue value)
    {
        LookInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        FireButtonPressed = value.isPressed;
        Debug.Log("Fire button pressed: " + FireButtonPressed);
    }
    
}
