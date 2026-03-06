using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform _mainCameraTransform;
    
    private void LateUpdate()
    {
        transform.forward = _mainCameraTransform.forward;
    }

    private void OnValidate()
    {
        if (_mainCameraTransform == null)
        {
            Debug.LogError("Main camera is null", this);
        }
    }
}