using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _pushForce = 2.0f;
    
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

        _characterController.Move(moveDirection * (_gameConfig.PlayerSpeed* Time.deltaTime));
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        
        if (body == null || body.isKinematic) return;
        if (hit.moveDirection.y < -0.3f) return;
        
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        
        body.AddForceAtPosition(pushDir * _pushForce, hit.point, ForceMode.Impulse);
    }
}
