using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content
{
    public class Spider :
        MoveComponent.ICondition,
        AttackComponent.ICondition,
        PushComponent.ICondition
    {
        private HealthComponent _healthComponent;
        
        public Spider(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }
        
        bool MoveComponent.ICondition.Invoke() => IsAlive();

        bool AttackComponent.ICondition.Invoke() => IsAlive();

        bool PushComponent.ICondition.Invoke() => IsAlive();

        private bool IsAlive() => _healthComponent.IsAlive;
    }
}