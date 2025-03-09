using Game.Scripts.GameObjects.Core;
using Zenject;

namespace Game.Scripts.GameObjects.Content.LavaObject
{
    public class MovingLava : IFixedTickable
    {
        private readonly WaypointMovementComponent _waypointMovementComponent;

        public MovingLava(WaypointMovementComponent waypointMovementComponent)
        {
            _waypointMovementComponent = waypointMovementComponent;
        }
        
        void IFixedTickable.FixedTick() => _waypointMovementComponent.Move();
    }
}