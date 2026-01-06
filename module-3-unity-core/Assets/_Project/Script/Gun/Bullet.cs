using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _lifeTime = 5f;
    private float _currentLifeTime = 0;
    private float _damage;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(float force, float damage, Vector3 direction)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(nameof(HandleLifeTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
          Target target = collision.gameObject.GetComponent<Target>();
          target.TakeDamage(_damage);
          Destroy(gameObject);
        }
    }


    IEnumerator HandleLifeTime()
    {
        _currentLifeTime = _lifeTime;

        while (_currentLifeTime > 0)
        {
            _currentLifeTime -= Time.deltaTime;
            yield return null;
        }
        if (_currentLifeTime <= 0)
        {
            Debug.Log("Miss, bullet destroyed");
            Destroy(gameObject);
        }
    }
}
