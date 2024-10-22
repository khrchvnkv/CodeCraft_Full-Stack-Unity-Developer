using System.Collections;
using Bullets;
using Characters.PlayerLogic;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.EnemyLogic
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private const int StartPoolSize = 7;

        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;
        [SerializeField] private Player _character;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private EnemyConstructor _prefab;
        [SerializeField] private BulletManager _bulletSystem;
        
        private Pool<EnemyConstructor> _enemyPool;
        
        private void Awake()
        {
            _enemyPool = new Pool<EnemyConstructor>(_prefab, _container);
            _enemyPool.Prepare(StartPoolSize);
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetRandomDelay());

                var enemy = _enemyPool.Spawn(_worldTransform);

                Transform spawnPosition = RandomPoint(_spawnPositions);
                enemy.transform.position = spawnPosition.position;

                Transform attackPosition = RandomPoint(_attackPositions);
                enemy.Construct(
                    _bulletSystem,
                    _character,
                    spawnPosition.position,
                    attackPosition.position, 
                    this);
            }
        }

        public void DespawnEnemy(EnemyConstructor enemy) => _enemyPool.Despawn(enemy);
        
        private float GetRandomDelay() => Random.Range(1.0f, 2.0f);

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}