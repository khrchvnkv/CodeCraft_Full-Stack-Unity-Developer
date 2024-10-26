namespace Characters.EnemyLogic.Factory
{
    public interface IEnemyDespawnCallback
    {
        void Destroy(in Enemy enemy);
    }
}