using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.TrapObject
{
    public class TrapConditions :
        PushComponent.ICondition,
        AttackComponent.ICondition
    {
        private bool IsGameObjectActive() => true;

        bool PushComponent.ICondition.Invoke() => IsGameObjectActive();

        bool AttackComponent.ICondition.Invoke() => IsGameObjectActive();
    }
}