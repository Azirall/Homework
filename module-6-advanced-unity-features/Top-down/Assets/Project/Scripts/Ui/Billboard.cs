using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }
    
    private void LateUpdate()
    {
        transform.forward = _mainCameraTransform.forward;
    }
}