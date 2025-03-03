using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.SnakeObject
{
    public class SnakeConditions :
        MoveComponent.ICondition,
        RotateComponent.ICondition,
        AttackComponent.ICondition,
        TossComponent.ICondition
    {
        private readonly HealthComponent _healthComponent;

        public SnakeConditions(HealthComponent healthComponent)
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