using Game.Views;
using Modules.Money;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetPopupPresenter : IInitializable
    {
        private readonly PlanetPopup _planetPopup;
        private readonly IMoneyStorage _moneyStorage;
        
        private Planet _planet;

        public PlanetPopupPresenter(
            PlanetPopup planetPopup,
            IMoneyStorage moneyStorage)
        {
            _planetPopup = planetPopup;
            _moneyStorage = moneyStorage;
        }

        void IInitializable.Initialize()
        {
            Hide();
        }

        public void Show(in Planet planet)
        {
            _planet = planet;
            UpdateView();
            
            _planetPopup.UpgradeButtonClicked += Upgrade;
            _planetPopup.CloseButtonClicked += Hide;
            _moneyStorage.OnMoneyChanged += UpdateUpgradeButtonInteractable;

            _planet.OnUpgraded += UpdateNewLevelView;
            _planet.OnUnlocked += UpdateIcon;
            _planet.OnPopulationChanged += UpdatePopulationText;
            _planet.OnUpgraded += UpdateLevelText;
            _planet.OnIncomeChanged += UpdateIncomeText;
            
            _planetPopup.Show();
        }

        public void Hide()
        {
            _planetPopup.UpgradeButtonClicked -= Upgrade;
            _planetPopup.CloseButtonClicked -= Hide;
            _moneyStorage.OnMoneyChanged -= UpdateUpgradeButtonInteractable;

            if (_planet != null)
            {
                _planet.OnUpgraded -= UpdateNewLevelView;
                _planet.OnUnlocked -= UpdateIcon;
                _planet.OnPopulationChanged -= UpdatePopulationText;
                _planet.OnUpgraded -= UpdateLevelText;
                _planet.OnIncomeChanged -= UpdateIncomeText;
            }

            _planetPopup.Hide();
        }

        private void UpdateView()
        {
            UpdateTitle();
            UpdateIcon();
            UpdatePopulationText(_planet.Population);
            UpdateLevelText(_planet.Level);
            UpdateIncomeText(_planet.MinuteIncome);
            UpdateMaxLevelUpgrade();
            UpdatePriceText();
            UpdateUpgradeButtonInteractable();
        }

        private void Upgrade()
        {
            if (_planet.CanUpgrade && _moneyStorage.IsEnough(_planet.Price))
            {
                _moneyStorage.Spend(_planet.Price);
                _planet.Upgrade();
            }
        }

        private void UpdateNewLevelView(int _) => UpdateView();

        private void UpdateTitle() => _planetPopup.SetTitleText(_planet.Name);

        private void UpdateIcon() => _planetPopup.SetIcon(_planet.GetIcon(_planet.IsUnlocked));

        private void UpdatePopulationText(int population)
        {
            const string format = "Population: {0}";
            _planetPopup.SetPopulationText(string.Format(format, population));
        }

        private void UpdateLevelText(int level)
        {
            const string format = "Level: {0}/{1}";
            _planetPopup.SetLevelText(string.Format(format, level, _planet.MaxLevel));
        }

        private void UpdateIncomeText(int income)
        {
            const string format = "Income: {0} / sec";
            _planetPopup.SetIncomeText(string.Format(format, income));
        }

        private void UpdateMaxLevelUpgrade() => _planetPopup.SetMaxUpgradeStatus(_planet.IsMaxLevel);
        
        private void UpdatePriceText()
        {
            var text = _planet.Price.ToString();
            _planetPopup.SetPriceText(text);
        }

        private void UpdateUpgradeButtonInteractable(int newvalue, int prevvalue) => 
            UpdateUpgradeButtonInteractable();

        private void UpdateUpgradeButtonInteractable() =>
            _planetPopup.UpdateUpgradeButtonInteractable(_planet.CanUpgrade && _moneyStorage.IsEnough(_planet.Price));
    }
}