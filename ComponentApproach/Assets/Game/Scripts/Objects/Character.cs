using Game.Scripts.Components;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Objects
{
    public sealed class Character : MonoBehaviour,
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
        private HealthComponent _healthComponent;
        
        public Rigidbody2D Rigidbody { get; private set; }

        [Inject]
        private void Construct(
            MoveComponent moveComponent,
            RotateComponent rotateComponent,
            HealthComponent healthComponent,
            Rigidbody2D rb)
        {
            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
            _healthComponent = healthComponent;
            Rigidbody = rb;
        }

        public void Move(in Vector2 direction)
        {
            _moveComponent.Move(direction);
            _rotateComponent.LookInDirection(direction);
        }

        bool JumpComponent.ICondition.Invoke() => IsAlive() && IsGrounded();

        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool RotateComponent.ICondition.Invoke() => IsAlive();

        bool PushComponent.ICondition.Invoke() => IsAlive();

        bool ThrowUpComponent.ICondition.Invoke() => IsAlive() && IsGrounded();

        private bool IsAlive() => _healthComponent.IsAlive;

        private bool IsGrounded() => _groundDetectComponent.IsGround();
    }
}