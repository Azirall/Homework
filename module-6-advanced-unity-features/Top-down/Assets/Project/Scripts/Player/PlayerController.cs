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
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_inputSystem.MoveInput.x,0 ,_inputSystem.MoveInput.y).normalized;
        
        if (_characterController.velocity.y >= 0)
        {
            moveDirection = new Vector3(_inputSystem.MoveInput.x,-_gameConfig.Gravity,_inputSystem.MoveInput.y);
        }

        _characterController.Move(moveDirection * (_gameConfig.PlayerSpeed* Time.fixedDeltaTime));
    }
}
