using System;
using Game.Scripts.Collision;
using Game.Scripts.Contracts;
using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class TrapController : IInitializable, IDisposable
    {
        private readonly Trap _trap;
        private readonly AttackCollision _attackCollision;

        public TrapController(
            Trap trap, 
            AttackCollision attackCollision)
        {
            _trap = trap;
            _attackCollision = attackCollision;
        }

        void IInitializable.Initialize() => _attackCollision.DamageableCollided += DamageableCollided;

        void IDisposable.Dispose() => _attackCollision.DamageableCollided -= DamageableCollided;

        private void DamageableCollided(IDamageable damageable, Rigidbody2D rb) => _trap.Attack(damageable, rb);
    }
}