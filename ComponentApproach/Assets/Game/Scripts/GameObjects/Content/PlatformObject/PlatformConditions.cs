using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.PlatformObject
{
    public class PlatformConditions : MoveComponent.ICondition
    {
        bool MoveComponent.ICondition.Invoke() => true;
    }
}