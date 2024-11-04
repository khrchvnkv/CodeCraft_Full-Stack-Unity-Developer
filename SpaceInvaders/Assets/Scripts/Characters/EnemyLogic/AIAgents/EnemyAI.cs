using Bullets.Factory;
using Characters.EnemyLogic.Factory;
using UnityEngine;

namespace Characters.EnemyLogic.AIAgents
{
    public sealed class EnemyAI : MonoBehaviour
    {
        [SerializeField] private MovementAgent _movementAgent;
        [SerializeField] private AttackAgent _attackAgent;
        [SerializeField] private EnemyDeathObserver _deathObserver;
        [SerializeField] private Ship _ship;

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
            _deathObserver.Construct(this, despawnCallback);
        }
        
        public void FixedUpdate()
        {
            if (!_movementAgent.TryMove())
            {
                _attackAgent.Shoot();
            }
        }
    }
}