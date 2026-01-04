using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _sensitivity = 0.1f;
    [SerializeField] private float _minPitch = -80f;
    [SerializeField] private float _maxPitch = 80f;

    private Vector2 _look;
    private float _pitch;

    private void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    private void Update()
    {
        float mouseX = _look.x * _sensitivity;
        float mouseY = _look.y * _sensitivity;

        _player.Rotate(Vector3.up * mouseX);

        _pitch = Mathf.Clamp(_pitch - mouseY, _minPitch, _maxPitch);
        _camera.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
