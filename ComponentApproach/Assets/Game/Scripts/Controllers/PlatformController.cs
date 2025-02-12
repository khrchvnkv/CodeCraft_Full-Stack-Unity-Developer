using Game.Scripts.Components;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class PlatformController : IFixedTickable
    {
        private readonly WaypointMovementComponent _waypointMovementComponent;

        public PlatformController(WaypointMovementComponent waypointMovementComponent)
        {
            _waypointMovementComponent = waypointMovementComponent;
        }

        void IFixedTickable.FixedTick() => _waypointMovementComponent.Move();
    }
}