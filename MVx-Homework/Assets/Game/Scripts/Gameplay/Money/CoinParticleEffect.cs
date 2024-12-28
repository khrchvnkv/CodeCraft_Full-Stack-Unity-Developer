using System;
using DG.Tweening;
using Game.Gameplay.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class CoinParticleEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        [Header("Animation")]
        [SerializeField] private float _animationDuration;
        [SerializeField] private Ease _animationEase;

        private void OnValidate() => 
            _rectTransform ??= gameObject.GetComponent<RectTransform>();

        public class Pool : MonoMemoryPool<Vector2, Vector2, Action, CoinParticleEffect>, ICoinParticleFactory
        {
            protected override void Reinitialize(Vector2 from, Vector2 to, Action callback, CoinParticleEffect effect)
            {
                effect._rectTransform.position = from;

                effect._rectTransform
                    .DOMove(to, effect._animationDuration)
                    .SetEase(effect._animationEase)
                    .OnComplete(OnComplete);

                void OnComplete()
                {
                    callback?.Invoke();
                    effect.DOKill();
                    Despawn(effect);
                }
            }

            public void SpawnEffect(in Vector2 from, in Vector2 to, Action callback = null) => 
                Spawn(from, to, callback);
        }
    }
}