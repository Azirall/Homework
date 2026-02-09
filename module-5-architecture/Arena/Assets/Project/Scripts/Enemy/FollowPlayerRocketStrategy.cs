using UnityEngine;

public class FollowPlayerRocketStrategy : IRocketStrategy
{
        private readonly Transform _playerTransform;
        private Rigidbody2D _rocketRb;
        private float _speed;
        public FollowPlayerRocketStrategy(Transform playerTransform, float moveSpeed)
        {
                _playerTransform = playerTransform;
                _speed = moveSpeed;
        }

        public void Init(Rigidbody2D rocketRb)
        {
                _rocketRb = rocketRb;
        }

        public void Handle()
        {
                Vector2 toPlayer = _playerTransform.position - _rocketRb.transform.position;
                
                float angle = Mathf.Atan2(_rocketRb.velocity.y, _rocketRb.velocity.x) * Mathf.Rad2Deg;
                _rocketRb.rotation = angle - 90f;
                
                _rocketRb.AddForce(toPlayer * _speed);
        }
}