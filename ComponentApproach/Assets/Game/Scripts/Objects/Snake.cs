using Game.Scripts.Components;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects
{
    public class Snake : MonoBehaviour,
        IAttackable,
        MoveComponent.ICondition,
        RotateComponent.ICondition,
        AttackComponent.ICondition,
        ThrowUpComponent.ICondition
    {
        private HealthComponent _healthComponent;
        private AttackComponent _attackComponent;
        private ThrowUpComponent _throwUpComponent;
        
        [Inject]
        private void Construct(
            HealthComponent healthComponent,
            AttackComponent attackComponent,
            ThrowUpComponent throwUpComponent)
        {
            _healthComponent = healthComponent;
            _attackComponent = attackComponent;
            _throwUpComponent = throwUpComponent;
        }
        
        void IAttackable.Attack(IDamageable damageable, Rigidbody2D rb)
        {
            _attackComponent.Attack(damageable);
            _throwUpComponent.ThrowUp(rb);
        }
        
        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool RotateComponent.ICondition.Invoke() => IsAlive();
        
        bool AttackComponent.ICondition.Invoke() => IsAlive();

        bool ThrowUpComponent.ICondition.Invoke() => IsAlive();

        private bool IsAlive() => _healthComponent.IsAlive;
    }
}