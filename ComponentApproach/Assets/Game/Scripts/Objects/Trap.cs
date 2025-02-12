using Game.Scripts.Components;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects
{
    public class Trap : MonoBehaviour,
        IKillable,
        IPlatformMovable,
        PushComponent.ICondition
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

        public void Attack(in IDamageable damageable, in Rigidbody2D rb)
        {
            _attackComponent.Attack(damageable);

            var direction = rb.position - Rigidbody.position;
            _pushComponent.Push(rb, direction);
        }

        void IKillable.Kill() => gameObject.SetActive(false);

        bool PushComponent.ICondition.Invoke() => gameObject.activeSelf;
    }
}