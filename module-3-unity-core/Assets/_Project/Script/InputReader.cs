using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public bool FireButtonPressed { get; private set; }
    public Vector2 Look {get; private set; }
    
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private PlayerInput _playerInput;
    
    private bool _menuOpened = false;
    private void Start()
    {
        Cursor.visible = false;
        _playerInput.SwitchCurrentActionMap("Gameplay");
    }
    
    private void OnLook(InputValue value)
    {
        Look = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
       FireButtonPressed = value.isPressed;
    }

    private void OnCancel(InputValue value)
    { 
        _menuOpened = !_menuOpened;
        
        Cursor.visible = _menuOpened;
        
        string inputMap = _menuOpened ? "Menu" : "Gameplay";
        
        _playerInput.SwitchCurrentActionMap(inputMap);
        
        _menuPanel.SetActive(_menuOpened);
    }
}
