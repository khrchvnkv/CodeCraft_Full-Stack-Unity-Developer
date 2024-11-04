using Characters.EnemyLogic.AIAgents;

namespace Characters.EnemyLogic.Factory
{
    public interface IEnemyDespawnCallback
    {
        void Destroy(in EnemyAI enemyAI);
    }
}