using System;
using DG.Tweening;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.Jump
{
    public class JumpViewComponent : IInitializable, IDisposable
    {
        [Serializable]
        public struct AnimationData
        {
            public float XSqueeze;
            public float YSqueeze;
            public float SqueezeDuration;
            public float UnsqueezeDuration;
            public Ease Ease;
        }

        private readonly JumpComponent _jumpComponent;
        private readonly Transform _transform;
        private readonly AnimationData _animationData;
        
        private Sequence _sequence;

        public JumpViewComponent(
            JumpComponent jumpComponent,
            Transform transform,
            AnimationData animationData)
        {
            _jumpComponent = jumpComponent;
            _transform = transform;
            _animationData = animationData;
        }

        void IInitializable.Initialize() => _jumpComponent.Jumped += UpdateJumpView;

        void IDisposable.Dispose()
        {
            if (_jumpComponent != null)
            {
                _jumpComponent.Jumped -= UpdateJumpView;
            }
        }

        private void UpdateJumpView()
        {
            if (_sequence == null)
            {
                _sequence = DOTween.Sequence();
                var originalScale = _transform.localScale;
                _sequence
                    .Append(_transform.DOScale(new Vector3(
                        originalScale.x * _animationData.XSqueeze, 
                        originalScale.y * _animationData.YSqueeze, 
                        originalScale.z), _animationData.SqueezeDuration))
                    .Append(_transform.DOScale(originalScale, _animationData.UnsqueezeDuration))
                    .SetEase(_animationData.Ease)
                    .SetAutoKill(false);
            }
            else
            {
                _sequence.Complete();
                _sequence.Restart();
            }
        }
    }
}