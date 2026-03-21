using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameConfig _gameConfig;

    private IInputService _inputService;

    private void Awake()
    {
        SetupInputService();
        SetupPlayerController();
    }

    private void SetupPlayerController()
    {
        var movementService = new Rigidbody2DMovementService(_rigidbody2D);
        _playerController.Initialize(_inputService, movementService, _gameConfig);
    }

    private void SetupInputService()
    {
        switch (_gameConfig.InputSource)
        {
            case InputSourceKind.Keyboard:
                _inputService = new KeyboardInputService(new PlayerInputService());
                break;
            case InputSourceKind.AI:
                break;
            case InputSourceKind.Other:
                break;
        }
    }

    private void OnValidate()
    {
        if (_playerController == null)
            Debug.LogError($"{nameof(Bootstrap)} requires {nameof(_playerController)} to be assigned.", this);

        if (_rigidbody2D == null)
            Debug.LogError($"{nameof(Bootstrap)} requires {nameof(_rigidbody2D)} to be assigned.", this);

        if (_gameConfig == null)
            Debug.LogError($"{nameof(Bootstrap)} requires {nameof(_gameConfig)} to be assigned.", this);
    }
}
