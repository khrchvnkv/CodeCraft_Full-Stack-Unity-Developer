using System.Collections.Generic;
using Bullets.Factory;
using Characters.PlayerLogic;
using Common;
using UnityEngine;

namespace Characters.EnemyLogic.Factory
{
    public class EnemyFactory : MonoBehaviour, IEnemyDespawnCallback
    {
        private const int StartPoolSize = 7;
        
        [SerializeField] private Player _character;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private BulletFactory _bulletFactory;

        public int TotalCount => _spawnedInstances.Count;
        
        private Pool<Enemy> _enemyPool;
        private HashSet<Enemy> _spawnedInstances = new();
        
        private void Awake()
        {
            _enemyPool = new Pool<Enemy>(_prefab, _container);
            _enemyPool.Prepare(StartPoolSize);
        }

        public Enemy SpawnEnemy(
            in Vector3 spawnPosition, 
            in Vector3 attackPosition)
        {
            var enemy = _enemyPool.Spawn(_worldTransform);
            enemy.transform.position = spawnPosition;
            enemy.Construct(
                _bulletFactory,
                _character,
                spawnPosition,
                attackPosition, 
                this);

            _spawnedInstances.Add(enemy);
            return enemy;
        }

        public void Destroy(in Enemy enemy)
        {
            _enemyPool.Despawn(enemy);
            _spawnedInstances.Remove(enemy);
        }
    }
}