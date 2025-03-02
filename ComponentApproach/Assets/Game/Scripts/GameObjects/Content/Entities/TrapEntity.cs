using Game.Scripts.GameObjects.Content.Contracts;
using Game.Scripts.GameObjects.Core;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Entities
{
    public class TrapEntity : Entity, IPlatformMovable, IAttackable, IDieable
    {
        public Rigidbody2D Rigidbody => _rigidbody2D ??= Get<Rigidbody2D>();

        private Rigidbody2D _rigidbody2D;
        
        void IAttackable.Attack(IDamageable damageable, Rigidbody2D rb)
        {
            Get<AttackComponent>().Attack(damageable);

            var direction = rb.position - Rigidbody.position;
            Get<PushComponent>().Push(rb, direction);
        }

        void IDieable.Die() => gameObject.SetActive(false);
    }
}