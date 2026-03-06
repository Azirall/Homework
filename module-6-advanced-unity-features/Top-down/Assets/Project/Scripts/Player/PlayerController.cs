using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private LayerMask _lookLayerMask;
    [SerializeField] private LayerMask _interactionLayerMask;
    
    private GameConfig _gameConfig;
    private InputSystem _inputSystem;
    private Camera _mainCamera;
    
    public void Init(InputSystem inputSystem, GameConfig gameConfig)
    {
        _inputSystem = inputSystem;
        _gameConfig = gameConfig;
        _mainCamera = Camera.main;
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

        _characterController.Move(moveDirection * (_gameConfig.PlayerSpeed* Time.deltaTime));
    }

    private void LookAtCursor()
    {
        Vector2 mousePos = _inputSystem.MousePosition;
        
        Ray ray = _mainCamera.ScreenPointToRay(mousePos);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _lookLayerMask))
        {
            Vector3 targetPoint = hit.point;
            
            targetPoint.y = transform.position.y; 
            
            transform.LookAt(targetPoint);
        }
    }
    
}
