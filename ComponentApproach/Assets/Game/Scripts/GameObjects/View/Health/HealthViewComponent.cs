using System;
using DG.Tweening;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.Health
{
    public class HealthViewComponent : IInitializable, IDisposable
    {
        [Serializable]
        public struct HealthViewArgs
        {
            public GameObject DeadDeactivatingGameObject;
            public SpriteRenderer SpriteRenderer;
            public Color DefaultColor;
            public Color DamageColor;
            public float AnimationDuration;
        }

        private readonly HealthComponent _healthComponent;
        private readonly HealthViewArgs _viewArgs;
        
        private Sequence _sequence;

        public HealthViewComponent(
            HealthComponent healthComponent,
            HealthViewArgs viewArgs)
        {
            _healthComponent = healthComponent;
            _viewArgs = viewArgs;
        }

        void IInitializable.Initialize()
        {
            _healthComponent.HealthPointsDecreased += PlayDamageView;
            _healthComponent.Died += PlayDieView;
        }

        void IDisposable.Dispose()
        {
            if (_healthComponent != null)
            {
                _healthComponent.HealthPointsDecreased -= PlayDamageView;
                _healthComponent.Died -= PlayDieView;
            }
            
            _sequence.Kill();
        }
        
        private void PlayDamageView()
        {
            if (_sequence == null)
            {
                _sequence = DOTween.Sequence();
                _sequence
                    .Append(_viewArgs.SpriteRenderer.DOColor(_viewArgs.DamageColor, _viewArgs.AnimationDuration / 2))
                    .Append(_viewArgs.SpriteRenderer.DOColor(_viewArgs.DefaultColor, _viewArgs.AnimationDuration / 2))
                    .SetAutoKill(false);
            }
            else
            {
                _sequence.Complete();
                _sequence.Restart();
            }
        }

        private void PlayDieView() => _viewArgs.DeadDeactivatingGameObject.SetActive(false);
    }
}