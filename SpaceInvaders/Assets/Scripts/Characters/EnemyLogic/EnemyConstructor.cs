using Bullets;
using Characters.PlayerLogic;
using Common;
using UnityEngine;

namespace Characters.EnemyLogic
{
    public sealed class EnemyConstructor : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _health;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _countdown;
        
        private BulletManager _bulletManager;
        private Player _target;
        private EnemyManager _enemyManager;
        private Vector2 _destination;
        private float _currentTime;
        private int _currentHealth;
        private bool _isPointReached;

        public bool IsAlive => _currentHealth > 0;
        private bool IsTimerExpired => _currentTime <= 0;

        public void Construct(
            in BulletManager bulletManager,
            in Player player, 
            in Vector2 startPoint,
            in Vector2 endPoint, 
            in EnemyManager enemyManager)
        {
            _bulletManager = bulletManager;
            _target = player;
            _rigidbody.MovePosition(startPoint);
            _destination = endPoint;
            _isPointReached = false;
            _enemyManager = enemyManager;
            ResetHealth();
            ResetTimer();
        }

        private void ResetHealth() => _currentHealth = _health;
        private void ResetTimer() => _currentTime = _countdown;

        private void FixedUpdate()
        {
            if (_isPointReached)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }

        private void Attack()
        {
            if (!_target.IsAlive)
                return;

            _currentTime -= Time.fixedDeltaTime;
            if (IsTimerExpired)
            {
                Vector2 startPosition = _firePoint.position;
                Vector2 vector = _target.Position - startPosition;
                Vector2 direction = vector.normalized;
                    
                _bulletManager.SpawnBullet(
                    startPosition,
                    Color.red,
                    (int) PhysicsLayer.ENEMY_BULLET,
                    1,
                    direction * 2
                );
                ResetTimer();
            }
        }

        private void Move()
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _rigidbody.MovePosition(_destination);
                _isPointReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody.position + direction * _speed;
            _rigidbody.MovePosition(nextPosition);
        }

        public void DealDamage(in int damage)
        {
            if (!IsAlive) return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            if (!IsAlive)
            {
                _enemyManager.DespawnEnemy(this);
            }
        }
    }
}