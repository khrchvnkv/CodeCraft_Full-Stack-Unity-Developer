using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Views.Components
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _maxLevelContainer;
        [SerializeField] private GameObject _upgradeContainer;
        [SerializeField] private TMP_Text _priceText;

        public event UnityAction OnClick
        {
            add => _button.onClick.AddListener(value);
            remove => _button.onClick.RemoveListener(value);
        }

        public void SetMaxLevel(in bool isMaxLevelUpgraded)
        {
            _maxLevelContainer.SetActive(isMaxLevelUpgraded);
            _upgradeContainer.SetActive(!isMaxLevelUpgraded);
        }

        public void SetPriceText(in string text) => _priceText.text = text;

        public void SetButtonInteractable(in bool interactable) => _button.interactable = interactable;
    }
}