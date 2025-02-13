using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class JumpViewComponent : MonoBehaviour
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
        
        [SerializeField] private AnimationData _animationData;
        [SerializeField] private Transform _transform;

        private JumpComponent _jumpComponent;

        private Sequence _sequence;

        [Inject]
        private void Construct(JumpComponent jumpComponent)
        {
            _jumpComponent = jumpComponent;
        }

        private void OnEnable() => _jumpComponent.Jumped += UpdateJumpView;

        private void OnDisable()
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