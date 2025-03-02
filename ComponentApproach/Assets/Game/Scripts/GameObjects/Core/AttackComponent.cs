 using Game.Scripts.GameObjects.Content.Contracts;

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

        public void Attack(IDamageable damageable)
        {
            if (_condition.Invoke())
            {
                damageable.TakeDamage(_damage);
            }
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}