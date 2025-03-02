using Game.Scripts.GameObjects.Content.Contracts;
using Game.Scripts.GameObjects.Core;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Entities
{
    public class CharacterEntity : Entity, IPlatformMovable, IDamageable, IDieable
    {
        public Rigidbody2D Rigidbody => _rigidbody2D ??= Get<Rigidbody2D>();

        private Rigidbody2D _rigidbody2D;

        void IDamageable.TakeDamage(in int damage) => Get<HealthComponent>().TakeDamage(damage);

        void IDieable.Die() => Get<HealthComponent>().Kill();
    }
}