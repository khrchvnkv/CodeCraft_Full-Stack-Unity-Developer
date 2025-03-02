using Game.Scripts.GameObjects.Content.Contracts;
using Game.Scripts.GameObjects.Core;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Entities
{
    public class SpiderEntity : Entity, IAttackable, IDamageable, IDieable
    {
        void IAttackable.Attack(IDamageable damageable, Rigidbody2D rb)
        {
            Get<AttackComponent>().Attack(damageable);
            
            var direction = rb.position - Get<Rigidbody2D>().position;
            Get<PushComponent>().Push(rb, direction);
        }
        
        void IDamageable.TakeDamage(in int damage) => Get<HealthComponent>().TakeDamage(damage);

        void IDieable.Die() => Get<HealthComponent>().Kill();
    }
}