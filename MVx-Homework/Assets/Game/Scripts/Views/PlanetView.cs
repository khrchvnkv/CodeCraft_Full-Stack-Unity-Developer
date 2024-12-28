using System;
using Game.Views.Components;
using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _lockGameObject;
        [SerializeField] private GameObject _priceContainer;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private RectTransform _readyIncomeRect;
        [SerializeField] private TimerProgressBar _incomeTimer;
        [SerializeField] private SmartButton _planetButton;

        public event Action OnButtonClick
        {
            add => _planetButton.OnClick += value;
            remove => _planetButton.OnClick -= value;
        }
        
        public event Action OnButtonHold
        {
            add => _planetButton.OnHold += value;
            remove => _planetButton.OnHold -= value;
        }

        public Vector2 GetIncomeIconPosition() => _readyIncomeRect.position;
        
        public void SetIcon(in Sprite sprite) => _icon.sprite = sprite;
        
        public void SetUnlock(in bool isUnlocked)
        {
            _lockGameObject.SetActive(!isUnlocked);
            _priceContainer.SetActive(!isUnlocked);
        }

        public void SetPriceText(in string text) => _priceText.text = text;

        public void SetReadyIncomeActivity(in bool isActive) => _readyIncomeRect.gameObject.SetActive(isActive);
       
        public void SetIncomeTimerActivity(in bool isActive) => _incomeTimer.SetActive(isActive);

        public void SetTimerValues(in string timerText, in float percent)
        {
            _incomeTimer.SetTimerText(timerText);
            _incomeTimer.SetTimerPercent(percent);
        }
    }
}