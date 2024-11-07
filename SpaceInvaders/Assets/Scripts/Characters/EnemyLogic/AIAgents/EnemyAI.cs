using Bullets.Factory;
using Characters.EnemyLogic.Factory;
using UnityEngine;

namespace Characters.EnemyLogic.AIAgents
{
    public sealed class EnemyAI : MonoBehaviour
    {
        [SerializeField] private MovementAgent _movementAgent;
        [SerializeField] private AttackAgent _attackAgent;
        [SerializeField] private Ship _ship;

        private IEnemyDespawnCallback _enemyDespawnCallback;
        
        public void Construct(
            in IEnemyDespawnCallback despawnCallback,
            in Ship player,
            in Vector3 startPosition,
            in Vector3 endPosition,
            in IBulletFactory bulletFactory)
        {
            _ship.ResetState();
            _ship.Construct(bulletFactory);
            _movementAgent.Construct(_ship, startPosition, endPosition);
            _attackAgent.Construct(_ship, player);
            _enemyDespawnCallback = despawnCallback;
        }

        private void OnEnable() => _ship.OnDied += OnShipDied;
        
        private void OnDisable() => _ship.OnDied -= OnShipDied;

        private void OnShipDied() => _enemyDespawnCallback?.Destroy(this);

        private void FixedUpdate()
        {
            if (!_movementAgent.TryMove())
            {
                _attackAgent.Shoot();
            }
        }
    }
}