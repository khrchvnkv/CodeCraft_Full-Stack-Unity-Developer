using Game.Scripts.Components;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects
{
    public class Spider : MonoBehaviour,
        IAttackable,
        MoveComponent.ICondition,
        AttackComponent.ICondition,
        PushComponent.ICondition
    {
        private Rigidbody2D _rigidbody;
        private HealthComponent _healthComponent;
        private AttackComponent _attackComponent;
        private PushComponent _pushComponent;
        
        [Inject]
        private void Construct(
            Rigidbody2D rb,
            HealthComponent healthComponent,
            AttackComponent attackComponent,
            PushComponent pushComponent)
        {
            _rigidbody = rb;
            _healthComponent = healthComponent;
            _attackComponent = attackComponent;
            _pushComponent = pushComponent;
        }
        
        void IAttackable.Attack(IDamageable damageable, Rigidbody2D rb)
        {
            _attackComponent.Attack(damageable);
            
            var direction = rb.position - _rigidbody.position;
            _pushComponent.Push(rb, direction);
        }

        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool AttackComponent.ICondition.Invoke() => IsAlive();

        bool PushComponent.ICondition.Invoke() => IsAlive();

        private bool IsAlive() => _healthComponent.IsAlive;
    }
}