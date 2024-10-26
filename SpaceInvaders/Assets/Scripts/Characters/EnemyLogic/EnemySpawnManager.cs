using System.Collections;
using Characters.EnemyLogic.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.EnemyLogic
{
    public sealed class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;
        [SerializeField] private EnemyFactory _enemyFactory;
        
        private IEnumerator Start()
        {
            while (_enemyFactory.TotalCount <= 5)
            {
                yield return new WaitForSeconds(GetRandomDelay());
                
                Vector3 spawnPosition = RandomPoint(_spawnPositions).position;
                Vector3 attackPosition = RandomPoint(_attackPositions).position;

                _enemyFactory.SpawnEnemy(spawnPosition, attackPosition);
            }
        }
        
        private float GetRandomDelay() => Random.Range(1.0f, 2.0f);

        private Transform RandomPoint(in Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}