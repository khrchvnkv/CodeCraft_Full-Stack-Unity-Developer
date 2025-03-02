using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content
{
    public sealed class Character : 
        JumpComponent.ICondition,
        MoveComponent.ICondition,
        RotateComponent.ICondition,
        PushComponent.ICondition, 
        TossComponent.ICondition
    {
        private readonly HealthComponent _healthComponent;
        private readonly GroundDetectorComponent _groundDetectorComponent;

        public Character(
            HealthComponent healthComponent,
            GroundDetectorComponent groundDetectorComponent)
        {
            _healthComponent = healthComponent;
            _groundDetectorComponent = groundDetectorComponent;
        }

        bool JumpComponent.ICondition.Invoke() => IsAlive() && IsGrounded();

        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool RotateComponent.ICondition.Invoke() => IsAlive();

        bool PushComponent.ICondition.Invoke() => IsAlive();

        bool TossComponent.ICondition.Invoke() => IsAlive() && IsGrounded();

        private bool IsAlive() => _healthComponent.IsAlive;

        private bool IsGrounded() => _groundDetectorComponent.IsGround();
    }
}