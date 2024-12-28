using System;
using DG.Tweening;
using Game.Views.Contracts;
using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour, ICoinParticleTarget
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private RectTransform _iconTransform;

        [Header("Animation")] 
        [SerializeField] private Ease _animationEase;
        [SerializeField] private float _animationDuration;

        private bool _changingLocked;
        private string _lastText;
        
        public void SetMoneyText(in string text)
        {
            if (IsAnimating())
            {
                _lastText = text;
                return;
            }
            
            SetText(text);
        }

        public void LockChanging() => _changingLocked = true;

        public Vector2 GetParticlePosition() => _iconTransform.position;
        
        public void ChangeMoneyWithAnimation(in int startValue, int endValue, Func<int, string> textGetter)
        {
            CheckTween();
            
            var value = startValue;
            _lastText = textGetter.Invoke(endValue);
            DOTween
                .To(
                    () => value,
                    x =>
                    {
                        value = x;
                        SetText(textGetter.Invoke(value));
                    },
                    endValue, _animationDuration)
                .SetEase(_animationEase)
                .OnComplete(() =>
                {
                    SetText(_lastText);
                    KillTween();
                });
        }

        private void SetText(in string text) => _moneyText.text = text;

        private bool IsAnimating() => _changingLocked || DOTween.IsTweening(this);

        private void CheckTween()
        {
            if (IsAnimating())
            {
                KillTween();
            }
        }

        private void KillTween()
        {
            _changingLocked = false;
            DOTween.Kill(this);
        }
    }
}