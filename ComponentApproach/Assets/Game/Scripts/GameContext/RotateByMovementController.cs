using System;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class RotateByMovementController : IInitializable, IDisposable
    {
        private readonly MoveComponent _moveComponent;
        private readonly RotateComponent _rotateComponent;

        public RotateByMovementController(MoveComponent moveComponent, RotateComponent rotateComponent)
        {
            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
        }

        void IInitializable.Initialize() => _moveComponent.MovedInDirection += UpdateRotation;

        void IDisposable.Dispose() => _moveComponent.MovedInDirection -= UpdateRotation;

        private void UpdateRotation(Vector2 direction) => _rotateComponent.LookInDirection(direction);
    }
}