using Game.Views.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Views
{
    public sealed class PlanetPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _populationText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _incomeText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private UpgradeButton _upgradeButton;

        public event UnityAction UpgradeButtonClicked
        {
            add => _upgradeButton.OnClick += value;
            remove => _upgradeButton.OnClick -= value;
        }

        public event UnityAction CloseButtonClicked
        {
            add => _closeButton.onClick.AddListener(value);
            remove => _closeButton.onClick.RemoveListener(value);
        }

        public void Show() => gameObject.SetActive(true);
        
        public void Hide() => gameObject.SetActive(false);

        public void SetTitleText(in string text) => _titleText.text = text;
        
        public void SetIcon(in Sprite icon) => _icon.sprite = icon;
        
        public void SetPopulationText(in string text) => _populationText.text = text;
        
        public void SetLevelText(in string text) => _levelText.text = text;
        
        public void SetIncomeText(in string text) => _incomeText.text = text;

        public void SetMaxUpgradeStatus(in bool isMaxLevel) => _upgradeButton.SetMaxLevel(isMaxLevel);
        
        public void SetPriceText(in string text) => _upgradeButton.SetPriceText(text);
        
        public void UpdateUpgradeButtonInteractable(in bool interactable) => _upgradeButton.SetButtonInteractable(interactable);
    }
}