using Game.Scripts.Components;

namespace Game.Scripts.Objects
{
    public class MovingLava : MoveComponent.ICondition
    {
        bool MoveComponent.ICondition.Invoke() => true;
    }
}