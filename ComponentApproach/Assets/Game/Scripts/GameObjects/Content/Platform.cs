using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content
{
    public class Platform : MoveComponent.ICondition
    {
        bool MoveComponent.ICondition.Invoke() => true;
    }
}