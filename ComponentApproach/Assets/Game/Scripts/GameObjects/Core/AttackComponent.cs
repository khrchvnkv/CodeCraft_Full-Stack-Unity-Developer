using Modules.Entity;

namespace Game.Scripts.GameObjects.Core
{
    public class AttackComponent
    {
        private readonly ICondition _condition;
        private readonly int _damage;

        public AttackComponent(
            ICondition condition,
            int damage)
        {
            _condition = condition;
            _damage = damage;
        }

        public void Attack(Entity entity)
        {
            if (_condition.Invoke() && entity.TryGet(out HealthComponent health))
            {
                health.TakeDamage(_damage);
            }
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}