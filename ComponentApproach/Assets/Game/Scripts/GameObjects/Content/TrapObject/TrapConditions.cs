using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.TrapObject
{
    public class TrapConditions :
        PushComponent.ICondition,
        AttackComponent.ICondition
    {
        private readonly DestroyableComponent _destroyableComponent;

        public TrapConditions(DestroyableComponent destroyableComponent)
        {
            _destroyableComponent = destroyableComponent;
        }

        private bool IsObjectNotDestroyable() => !_destroyableComponent.IsDestroyable;

        bool PushComponent.ICondition.Invoke() => IsObjectNotDestroyable();

        bool AttackComponent.ICondition.Invoke() => IsObjectNotDestroyable();
    }
}