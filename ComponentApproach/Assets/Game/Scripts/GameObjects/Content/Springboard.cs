using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content
{
    public class Springboard : TossComponent.ICondition
    {
        bool TossComponent.ICondition.Invoke() => true;
    }
}