using Characters.EnemyLogic.AIAgents;
using Characters.EnemyLogic.Factory;

namespace Characters.EnemyLogic
{
    public class EnemyDeathObserver : DeathObserver
    {
        private EnemyAI _enemyAI;
        private IEnemyDespawnCallback _despawnCallback;

        public void Construct(
            in EnemyAI enemyAI,
            in IEnemyDespawnCallback despawnCallback)
        {
            _enemyAI = enemyAI;
            _despawnCallback = despawnCallback;
        }

        protected override void Died() => _despawnCallback?.Destroy(_enemyAI);
    }
}