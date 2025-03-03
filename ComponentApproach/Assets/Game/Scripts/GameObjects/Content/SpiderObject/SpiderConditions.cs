using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.SpiderObject
{
    public class SpiderConditions :
        MoveComponent.ICondition,
        AttackComponent.ICondition,
        PushComponent.ICondition
    {
        private readonly HealthComponent _healthComponent;

        public SpiderConditions(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool AttackComponent.ICondition.Invoke() => IsAlive();

        bool PushComponent.ICondition.Invoke() => IsAlive();

        private bool IsAlive() => _healthComponent.IsAlive;
    }
}