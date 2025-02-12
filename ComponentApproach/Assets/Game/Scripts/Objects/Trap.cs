using Game.Scripts.Components;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects
{
    public class Trap : MonoBehaviour,
        IDieable,
        IPlatformMovable,
        IAttackable,
        PushComponent.ICondition,
        AttackComponent.ICondition
    {
        private PushComponent _pushComponent;
        private AttackComponent _attackComponent;
        
        public Rigidbody2D Rigidbody { get; private set; }

        [Inject]
        private void Construct(
            Rigidbody2D rb,
            PushComponent pushComponent, 
            AttackComponent attackComponent)
        {
            Rigidbody = rb;
            _pushComponent = pushComponent;
            _attackComponent = attackComponent;
        }

        void IAttackable.Attack(IDamageable damageable, Rigidbody2D rb)
        {
            _attackComponent.Attack(damageable);

            var direction = rb.position - Rigidbody.position;
            _pushComponent.Push(rb, direction);
        }

        void IDieable.Die() => gameObject.SetActive(false);

        bool PushComponent.ICondition.Invoke() => IsGameObjectActive();

        bool AttackComponent.ICondition.Invoke() => IsGameObjectActive();

        private bool IsGameObjectActive() => gameObject.activeSelf;
    }
}