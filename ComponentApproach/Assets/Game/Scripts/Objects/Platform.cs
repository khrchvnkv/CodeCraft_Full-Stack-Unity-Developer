using Game.Scripts.Components;

namespace Game.Scripts.Objects
{
    public class Platform : MoveComponent.ICondition
    {
        bool MoveComponent.ICondition.Invoke() => true;
    }
}