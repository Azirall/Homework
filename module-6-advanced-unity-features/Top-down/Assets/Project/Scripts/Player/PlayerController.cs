using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    
    private GameConfig _gameConfig;
    private InputSystem _inputSystem;
    
    public void Init(InputSystem inputSystem, GameConfig gameConfig)
    {
        _inputSystem = inputSystem;
        _gameConfig = gameConfig;
    }

    private void Update()
    {
        Move();
        LookAtCursor();
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.forward * _inputSystem.MoveInput.y + Vector3.right * _inputSystem.MoveInput.x;
        
        if (!_characterController.isGrounded)
        {
            moveDirection.y = -2f;
        }

        _characterController.Move(moveDirection * (_gameConfig.PlayerSpeed* Time.fixedDeltaTime));
    }

    private void LookAtCursor()
    {
        Vector2 mousePos = _inputSystem.MousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            Vector3 targetPoint = hit.point;
            
            targetPoint.y = transform.position.y; 
            
            transform.LookAt(targetPoint);
        }
    }
}
