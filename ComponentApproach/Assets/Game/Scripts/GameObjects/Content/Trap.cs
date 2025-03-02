using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content
{
    public class Trap :
        PushComponent.ICondition,
        AttackComponent.ICondition
    {
        bool PushComponent.ICondition.Invoke() => IsGameObjectActive();

        bool AttackComponent.ICondition.Invoke() => IsGameObjectActive();

        private bool IsGameObjectActive() => true;
    }
}