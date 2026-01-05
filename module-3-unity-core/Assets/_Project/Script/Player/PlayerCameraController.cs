using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _sensitivity = 0.1f;
    [SerializeField] private float _minPitch = -80f;
    [SerializeField] private float _maxPitch = 80f;
    
    private InputReader _inputReader;
    private float _pitch;

    [Inject]
    public void Construct(InputReader inputReader)
    {
        _inputReader = inputReader;
    }

    private void Update()
    {
        float mouseX = _inputReader.Look.x * _sensitivity;
        float mouseY = _inputReader.Look.y * _sensitivity;

        _player.Rotate(Vector3.up * mouseX);

        _pitch = Mathf.Clamp(_pitch - mouseY, _minPitch, _maxPitch);
        _camera.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
