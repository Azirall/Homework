using UnityEngine;

public class FollowPlayerRocketStrategy : IRocketStrategy
{
        private Transform _playerTransform;
        private Rigidbody2D _rocketRb;
        private float _speed;
        public FollowPlayerRocketStrategy(Rigidbody2D rocketRb, Transform playerTransform, float moveSpeed)
        {
                _playerTransform = playerTransform;
                _rocketRb = rocketRb;
                _speed = moveSpeed;
                
        }

        public void Handle()
        {
                Vector2 toPlayer = _playerTransform.position - _rocketRb.transform.position;
                
                float angle = Mathf.Atan2(_rocketRb.velocity.y, _rocketRb.velocity.x) * Mathf.Rad2Deg;
                _rocketRb.rotation = angle - 90f;
                
                _rocketRb.AddForce(toPlayer * _speed);
        }
}