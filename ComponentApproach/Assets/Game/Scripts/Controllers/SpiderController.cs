using Game.Scripts.Components;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class SpiderController : IFixedTickable
    {
        private readonly WaypointMovementComponent _waypointMovementComponent;

        public SpiderController(WaypointMovementComponent waypointMovementComponent)
        {
            _waypointMovementComponent = waypointMovementComponent;
        }
        
        void IFixedTickable.FixedTick() => _waypointMovementComponent.Move();
    }
}