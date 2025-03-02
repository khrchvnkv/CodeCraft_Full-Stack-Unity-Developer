using Game.Scripts.GameObjects.Core;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class WaypointMovementController : IFixedTickable
    {
        private readonly WaypointMovementComponent _waypointMovementComponent;

        public WaypointMovementController(WaypointMovementComponent waypointMovementComponent)
        {
            _waypointMovementComponent = waypointMovementComponent;
        }

        void IFixedTickable.FixedTick() => _waypointMovementComponent.Move();
    }
}