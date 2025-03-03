using Game.Scripts.GameObjects.Core;

namespace Game.Scripts.GameObjects.Content.SpringboardObject
{
    public class SpringboardConditions : TossComponent.ICondition
    {
        bool TossComponent.ICondition.Invoke() => true;
    }
}