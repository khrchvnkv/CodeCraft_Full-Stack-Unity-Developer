using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class HealthViewComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _deadDeactivatingGameObject;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _damageColor;
        [SerializeField] private float _animationDuration;

        private HealthComponent _healthComponent;
        private Sequence _sequence;

        [Inject]
        private void Construct(
            HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        private void OnEnable()
        {
            _healthComponent.HealthPointsDecreased += PlayDamageView;
            _healthComponent.Died += PlayDieView;
        }

        private void OnDisable()
        {
            if (_healthComponent != null)
            {
                _healthComponent.HealthPointsDecreased -= PlayDamageView;
                _healthComponent.Died -= PlayDieView;
            }
        }

        private void OnDestroy() => _sequence.Kill();

        private void PlayDamageView()
        {
            if (_sequence == null)
            {
                _sequence = DOTween.Sequence();
                _sequence
                    .Append(_spriteRenderer.DOColor(_damageColor, _animationDuration / 2))
                    .Append(_spriteRenderer.DOColor(_defaultColor, _animationDuration / 2))
                    .SetAutoKill(false);
            }
            else
            {
                _sequence.Complete();
                _sequence.Restart();
            }
        }

        private void PlayDieView() => _deadDeactivatingGameObject.SetActive(false);
    }
}