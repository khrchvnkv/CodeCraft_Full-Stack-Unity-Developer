using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.LavaObject
{
    public class MovingLavaConditions : MoveComponent.ICondition
    {
        bool MoveComponent.ICondition.Invoke() => true;
    }
}