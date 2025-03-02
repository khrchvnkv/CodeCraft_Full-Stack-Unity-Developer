using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content
{
    public class Snake : 
        MoveComponent.ICondition,
        RotateComponent.ICondition,
        AttackComponent.ICondition,
        TossComponent.ICondition
    {
        private HealthComponent _healthComponent;
        
        public Snake(
            HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool RotateComponent.ICondition.Invoke() => IsAlive();
        
        bool AttackComponent.ICondition.Invoke() => IsAlive();

        bool TossComponent.ICondition.Invoke() => IsAlive();

        private bool IsAlive() => _healthComponent.IsAlive;
    }
}