using UnityEngine;

namespace Game.Scripts.Components
{
    public class WaypointMovementComponent
    {
        private const float StoppingDistance = 0.01f;

        private readonly MoveComponent _moveComponent;
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform[] _waypoints;

        private int _waypointIndex;

        private Transform Waypoint => _waypoints[_waypointIndex];

        public WaypointMovementComponent(
            MoveComponent moveComponent,
            Rigidbody2D rigidbody,
            Transform[] waypoints)
        {
            _moveComponent = moveComponent;
            _rigidbody = rigidbody;
            _waypoints = waypoints;
        }

        public void Move()
        {
            var position = _rigidbody.position;
            var direction = (Vector2)Waypoint.position - position;
            _moveComponent.Move(direction);

            if (IsReached())
            {
                IncreaseWaypointIndex();
            }
        }

        private void IncreaseWaypointIndex() => _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;

        private bool IsReached()
        {
            var sqrMagnitude = ((Vector2)Waypoint.position - _rigidbody.position).sqrMagnitude;
            return sqrMagnitude <= StoppingDistance;
        }
    }
}