using Game.Scripts.Contracts;

namespace Game.Scripts.Components
{
    public class AttackComponent
    {
        private readonly int _damage;

        public AttackComponent(int damage)
        {
            _damage = damage;
        }

        public void Attack(IDamageable damageable) => damageable.TakeDamage(_damage);
    }
}