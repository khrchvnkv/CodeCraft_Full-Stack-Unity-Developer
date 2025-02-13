using Game.Scripts.Components;
using Zenject;

namespace Game.Scripts.Controllers
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