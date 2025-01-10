using Game.Views.Components;
using Game.Views.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private IPlanetPopupPresenter _presenter;

        [Inject]
        private void Construct(IPlanetPopupPresenter presenter)
        {
            _presenter = presenter;

            Initialize();
        }

        private void Initialize()
        {
            _presenter.PopupShowed += Show;
            Hide();
        }
        
        private void OnEnable()
        {
            UpdateView();

            _presenter.ButtonInteractableUpdated += UpdateUpgradeButtonInteractable;
            _presenter.PlanetUpgraded += UpdateView;
            _presenter.PlanetUnlocked += UpdateIcon;
            _presenter.PlanetPopulationChanged += UpdatePopulationText;
            _presenter.PlanetIncomeChanged += UpdateIncomeText;

            _upgradeButton.OnClick += Upgrade;
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _presenter.ButtonInteractableUpdated += UpdateUpgradeButtonInteractable;
            _presenter.PlanetUpgraded += UpdateView;
            _presenter.PlanetUnlocked += UpdateIcon;
            _presenter.PlanetPopulationChanged += UpdatePopulationText;
            _presenter.PlanetIncomeChanged += UpdateIncomeText;
            
            _upgradeButton.OnClick -= Upgrade;
            _closeButton.onClick.RemoveListener(Hide);
        }

        private void OnDestroy() => _presenter.PopupShowed -= Show;

        private void UpdateView()
        {
            UpdateTitleText();
            UpdateIcon();
            UpdatePopulationText();
            UpdateLevelText();
            UpdateIncomeText();
            UpdateMaxUpgradeStatus();
            UpdatePriceText();
            UpdateUpgradeButtonInteractable();
        }

        private void Show() => gameObject.SetActive(true);
        
        private void Hide()
        {
            _presenter.Hide();
            gameObject.SetActive(false);
        }

        private void Upgrade() => _presenter.UpgradePlanet();

        private void UpdateTitleText() => _titleText.text = _presenter.GetTitleText();
        
        private void UpdateIcon() => _icon.sprite = _presenter.GetPlanetIcon();
        
        private void UpdatePopulationText() => _populationText.text = _presenter.GetPopulationText();
        
        private void UpdateLevelText() => _levelText.text = _presenter.GetLevelText();
        
        private void UpdateIncomeText() => _incomeText.text = _presenter.GetIncomeText();

        private void UpdateMaxUpgradeStatus() => _upgradeButton.SetMaxLevel(_presenter.IsMaxLevelUpgrade());
        
        private void UpdatePriceText() => _upgradeButton.SetPriceText(_presenter.GetPriceText());
        
        private void UpdateUpgradeButtonInteractable() => _upgradeButton.SetButtonInteractable(_presenter.IsUpgradeButtonInteractable());
    }
}