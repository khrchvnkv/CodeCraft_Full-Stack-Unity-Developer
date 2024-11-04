using System.Collections.Generic;
using Bullets.Factory;
using Characters.EnemyLogic.AIAgents;
using Common;
using UnityEngine;

namespace Characters.EnemyLogic.Factory
{
    public class EnemyFactory : MonoBehaviour, IEnemyDespawnCallback
    {
        private const int StartPoolSize = 7;
        
        [SerializeField] private Ship _character;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private EnemyAI _prefab;
        [SerializeField] private BulletFactory _bulletFactory;

        public int TotalCount => _spawnedInstances.Count;
        
        private Pool<EnemyAI> _enemyPool;
        private HashSet<EnemyAI> _spawnedInstances = new();
        
        private void Awake()
        {
            _enemyPool = new Pool<EnemyAI>(_prefab, _container);
            _enemyPool.Prepare(StartPoolSize);
        }

        public EnemyAI SpawnEnemy(
            in Vector3 spawnPosition, 
            in Vector3 attackPosition)
        {
            var enemy = _enemyPool.Spawn(_worldTransform);
            enemy.transform.position = spawnPosition;
            enemy.Construct(
                this,
                _character,
                spawnPosition,
                attackPosition,
                _bulletFactory);

            _spawnedInstances.Add(enemy);
            return enemy;
        }

        void IEnemyDespawnCallback.Destroy(in EnemyAI enemyAI)
        {
            _enemyPool.Despawn(enemyAI);
            _spawnedInstances.Remove(enemyAI);
        }
    }
}