using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private Action<Bullet> _despawnCallback;

    public void Init(Action<Bullet> despawnCallback)
    {
        _despawnCallback = despawnCallback;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_despawnCallback != null)
        {
            _despawnCallback(this);
        }
    }
}