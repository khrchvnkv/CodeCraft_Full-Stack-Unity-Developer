using System.Collections.Generic;
using Game.Scripts.Components;
using Game.Scripts.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class CharacterController : ITickable, IFixedTickable
    {
        private const string HorizontalAxis = "Horizontal";
        
        private readonly Rigidbody2D _rigidbody;
        private readonly MoveComponent _moveComponent;
        private readonly RotateComponent _rotateComponent;
        private readonly PushTrigger _pushTrigger;
        private readonly PushComponent _pushComponent;
        private readonly ThrowUpComponent _throwUpComponent;
        private readonly JumpComponent _jumpComponent;

        private Vector2 _movementInput;
        private bool _isJumpRequest;
        private bool _isPushRequest;
        private bool _isThrowUpRequest;

        private bool HasInput => _movementInput.sqrMagnitude > Mathf.Epsilon * Mathf.Epsilon;

        public CharacterController(
            Rigidbody2D rigidbody,
            MoveComponent moveComponent,
            RotateComponent rotateComponent,
            PushTrigger pushTrigger,
            PushComponent pushComponent,
            ThrowUpComponent throwUpComponent,
            JumpComponent jumpComponent)
        {
            _rigidbody = rigidbody;
            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
            _pushTrigger = pushTrigger;
            _pushComponent = pushComponent;
            _throwUpComponent = throwUpComponent;
            _jumpComponent = jumpComponent;
        }

        void ITickable.Tick() => ReadMovementInput();

        void IFixedTickable.FixedTick() => HandleMovementInput();

        private void ReadMovementInput()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            _movementInput = new Vector2(horizontal, 0);

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumpRequest)
            {
                _isJumpRequest = true;
            }
            
            if (Input.GetMouseButtonDown(0) && !_isPushRequest)
            {
                _isPushRequest = true;
            }
            
            if (Input.GetMouseButtonDown(1) && !_isThrowUpRequest)
            {
                _isThrowUpRequest = true;
            }
        }

        private void HandleMovementInput()
        {
            if (HasInput)
            {
                _moveComponent.Move(_movementInput);
                _rotateComponent.LookInDirection(_movementInput);
            }

            if (_isJumpRequest)
            {
                _jumpComponent.Jump();
                _isJumpRequest = false;  
            }

            PushOrThrowUp();
        }

        private void PushOrThrowUp()
        {
            if (!_isPushRequest && !_isThrowUpRequest)
            {
                return;
            }

            var targets = GetTargetsCollection();
            if (_isPushRequest)
            {
                _pushComponent.Push(targets, _rigidbody.position);
            }

            if (_isThrowUpRequest)
            {
                _throwUpComponent.ThrowUp(targets);
            }

            _isPushRequest = false;
            _isThrowUpRequest = false;
        }

        private IReadOnlyCollection<Rigidbody2D> GetTargetsCollection() => _pushTrigger.Targets;
    }
}