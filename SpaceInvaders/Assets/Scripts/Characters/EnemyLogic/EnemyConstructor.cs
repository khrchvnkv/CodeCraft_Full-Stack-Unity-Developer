using Bullets;
using Characters.PlayerLogic;
using UnityEngine;

namespace Characters.EnemyLogic
{
    [RequireComponent(
        typeof(EnemyMovement),
        typeof(EnemyShooting),
        typeof(HealthComponent))]
    public sealed class EnemyConstructor : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyShooting _shooting;
        [SerializeField] private HealthComponent _healthComponent;

        private EnemyManager _enemyManager;

        private void OnValidate()
        {
            _movement ??= gameObject.GetComponent<EnemyMovement>();
            _shooting ??= gameObject.GetComponent<EnemyShooting>();
            _healthComponent ??= gameObject.GetComponent<HealthComponent>();
        }

        public void Construct(
            in BulletManager bulletManager,
            in PlayerConstructor playerShooting, 
            in Vector2 startPoint,
            in Vector2 endPoint, 
            in EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
            
            _movement.Construct(startPoint, endPoint);
            _shooting.Construct(bulletManager, playerShooting);
            _healthComponent.ResetHealth();

            _healthComponent.OnHealthEmpty += Died;
        }

        private void Died()
        {
            _healthComponent.OnHealthEmpty -= Died;
            _enemyManager.DespawnEnemy(this);
        }

        private void FixedUpdate()
        {
            if (!_movement.TryMove())
            {
                _shooting.Shoot();
            }
        }
    }
}