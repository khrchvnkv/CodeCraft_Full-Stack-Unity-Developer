using Game.Scripts.Components;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects
{
    public sealed class Character : MonoBehaviour,
        IDamageable,
        IKillable,
        IPlatformMovable,
        JumpComponent.ICondition,
        MoveComponent.ICondition,
        RotateComponent.ICondition,
        PushComponent.ICondition, 
        ThrowUpComponent.ICondition
    {
        [SerializeField] private GroundDetectComponent _groundDetectComponent;

        private MoveComponent _moveComponent;
        private RotateComponent _rotateComponent;
        private JumpComponent _jumpComponent;
        private HealthComponent _healthComponent;
        private PushComponent _pushComponent;
        private ThrowUpComponent _throwUpComponent;
        
        public Rigidbody2D Rigidbody { get; private set; }

        [Inject]
        private void Construct(
            MoveComponent moveComponent,
            RotateComponent rotateComponent,
            JumpComponent jumpComponent,
            HealthComponent healthComponent,
            PushComponent pushComponent,
            ThrowUpComponent throwUpComponent,
            Rigidbody2D rb)
        {
            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
            _jumpComponent = jumpComponent;
            _healthComponent = healthComponent;
            _pushComponent = pushComponent;
            _throwUpComponent = throwUpComponent;
            Rigidbody = rb;
        }

        public void Move(in Vector2 direction)
        {
            _moveComponent.Move(direction);
            _rotateComponent.LookInDirection(direction);
        }

        public void Jump() => _jumpComponent.Jump();

        public void Push(in Rigidbody2D rb, in Vector2 direction) => _pushComponent.Push(rb, direction);

        public void ThrowUp(Rigidbody2D rb) => _throwUpComponent.ThrowUp(rb);

        void IDamageable.TakeDamage(in int damage) => _healthComponent.TakeDamage(damage);

        void IKillable.Kill() => _healthComponent.Kill();

        bool JumpComponent.ICondition.Invoke() => IsAlive() && IsGrounded();

        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool RotateComponent.ICondition.Invoke() => IsAlive();

        bool PushComponent.ICondition.Invoke() => IsAlive();

        bool ThrowUpComponent.ICondition.Invoke() => IsAlive() && IsGrounded();

        private bool IsAlive() => _healthComponent.IsAlive;

        private bool IsGrounded() => _groundDetectComponent.IsGround();
    }
}