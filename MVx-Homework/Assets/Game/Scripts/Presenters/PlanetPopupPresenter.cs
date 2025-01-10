using System;
using Game.Views.Contracts;
using Modules.Money;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public sealed class PlanetPopupPresenter : IInitializable, IPlanetPopupPresenter
    {
        private readonly IMoneyStorage _moneyStorage;
        
        private Planet _planet;

        public event Action ButtonInteractableUpdated;
        public event Action PlanetUpgraded;
        public event Action PlanetUnlocked;
        public event Action PlanetPopulationChanged;
        public event Action PlanetIncomeChanged;
        public event Action PopupShowed;

        public PlanetPopupPresenter(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        void IInitializable.Initialize()
        {
            Hide();
        }

        public void Show(in Planet planet)
        {
            _planet = planet;
            
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;

            _planet.OnUpgraded += OnUpgraded;
            _planet.OnUnlocked += OnUnlocked;
            _planet.OnPopulationChanged += OnPopulationChanged;
            _planet.OnIncomeChanged += OnIncomeChanged;
            
            PopupShowed?.Invoke();
        }

        public void Hide()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;

            if (_planet != null)
            {
                _planet.OnUpgraded -= OnUpgraded;
                _planet.OnUnlocked -= OnUnlocked;
                _planet.OnPopulationChanged -= OnPopulationChanged;
                _planet.OnIncomeChanged -= OnIncomeChanged;
            }
        }

        public void UpgradePlanet() => _planet.Upgrade();

        public Sprite GetPlanetIcon() => _planet.GetIcon(_planet.IsUnlocked); 
        
        public string GetTitleText() => _planet.Name;
        
        public string GetPopulationText()
        {
            const string format = "Population: {0}";
            return string.Format(format, _planet.Population);
        }

        public string GetLevelText()
        {
            const string format = "Level: {0}/{1}";
            return string.Format(format, _planet.Level, _planet.MaxLevel);
        }

        public string GetIncomeText()
        {
            const string format = "Income: {0} / sec";
            return string.Format(format, _planet.MinuteIncome);
        }

        public bool IsMaxLevelUpgrade() => _planet.IsMaxLevel;
        
        public string GetPriceText()
        {
            var text = _planet.Price.ToString();
            return text;
        }

        public bool IsUpgradeButtonInteractable() => _planet.CanUpgrade;
        
        private void OnMoneyChanged(int _, int __) => ButtonInteractableUpdated?.Invoke();
        
        private void OnUpgraded(int _) => PlanetUpgraded?.Invoke();
        
        private void OnUnlocked() => PlanetUnlocked?.Invoke();

        private void OnPopulationChanged(int _) => PlanetPopulationChanged?.Invoke();

        private void OnIncomeChanged(int _) => PlanetIncomeChanged?.Invoke();
    }
}