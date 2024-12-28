using System;
using System.Linq;
using DG.Tweening;
using Game.Gameplay.Contracts;
using Game.Views;
using Game.Views.Contracts;
using Modules.Money;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly PlanetView _planetView;
        private readonly Planet _planet;
        private readonly PlanetPopupPresenter _popupPresenter;
        private readonly MoneyPresenter _moneyPresenter;
        private readonly IMoneyStorage _moneyStorage;
        private readonly ICoinParticleFactory _particleFactory;
        private readonly ICoinParticleTarget _particleTarget;

        public PlanetPresenter(
            Planet[] planets,
            PlanetPopupPresenter popupPresenter,
            MoneyPresenter moneyPresenter,
            IMoneyStorage moneyStorage,
            ICoinParticleFactory particleFactory,
            ICoinParticleTarget particleTarget,
            PlanetView view,
            string planetName)
        {
            _planet = planets.First(x => x.Name == planetName);
            _popupPresenter = popupPresenter;
            _moneyPresenter = moneyPresenter;
            _moneyStorage = moneyStorage;
            _particleFactory = particleFactory;
            _particleTarget = particleTarget;
            _planetView = view;
        }

        void IInitializable.Initialize()
        {
            UpdateView();

            _planetView.OnButtonClick += ProcessPlanetClick;
            _planetView.OnButtonHold += ShowPopup;
            
            _planet.OnUnlocked += UpdateView;
            _planet.OnIncomeReady += UpdateIncomeReady;
            _planet.OnIncomeTimeChanged += UpdateIncomeTimer;
        }

        void IDisposable.Dispose()
        {
            _planetView.OnButtonClick -= ProcessPlanetClick;
            _planetView.OnButtonHold -= ShowPopup;
            
            _planet.OnUnlocked -= UpdateView;
            _planet.OnIncomeReady -= UpdateIncomeReady;
            _planet.OnIncomeTimeChanged -= UpdateIncomeTimer;
        }

        private void ProcessPlanetClick()
        {
            if (_planet.IsUnlocked)
            {
                if (_planet.IsIncomeReady)
                {
                    ShowCoinParticle();
                    _planet.GatherIncome();
                }
            }
            else
            {
                PurchasePlanet();
            }
        }

        private void ShowPopup()
        {
            if (_planet.IsUnlocked)
            {
                _popupPresenter.Show(_planet);
            }
        }

        private void PurchasePlanet()
        {
            if (_moneyStorage.IsEnough(_planet.Price))
            {
                _moneyStorage.Spend(_planet.Price);
                _planet.Unlock();
            }
        }

        private void UpdateView()
        {
            UpdateUnlock();
            UpdatePrice();
            UpdateReadyIncomeStatus();
            UpdateIncomeTimerStatus();
        }

        private void UpdateIcon() => _planetView.SetIcon(_planet.GetIcon(_planet.IsUnlocked));

        private void UpdateUnlock()
        {
            UpdateIcon();
            _planetView.SetUnlock(_planet.IsUnlocked);
        }

        private void UpdatePrice() => _planetView.SetPriceText(_planet.Price.ToString());

        private void UpdateReadyIncomeStatus()
        {
            var isActive = _planet.IsUnlocked && _planet.IsIncomeReady;
            _planetView.SetReadyIncomeActivity(isActive);
        }

        private void UpdateIncomeTimerStatus()
        {
            var isActive = _planet.IsUnlocked && !_planet.IsIncomeReady;
            _planetView.SetIncomeTimerActivity(isActive);
        }

        private void UpdateIncomeReady(bool isReady)
        {
            _planetView.SetReadyIncomeActivity(isReady);
            _planetView.SetIncomeTimerActivity(!isReady);
        }

        private void UpdateIncomeTimer(float value)
        {
            const string format = "{0}m:{1}s";
            
            var intValue = (int)value;
            int minutes = intValue / 60;
            int seconds = intValue % 60;
            var text = string.Format(format, minutes, seconds);

            _planetView.SetTimerValues(text, _planet.IncomeProgress);
        }

        private void ShowCoinParticle()
        {
            var money = _moneyStorage.Money;
            var startValue = money;
            var endValue = money + _planet.MinuteIncome;
            
            _moneyPresenter.LockChanging();
            _particleFactory.SpawnEffect(_planetView.GetIncomeIconPosition(), _particleTarget.GetParticlePosition(), CallBack);

            void CallBack() => _moneyPresenter.UpdateMoneyWithAnimation(startValue, endValue);
        }
    }
}