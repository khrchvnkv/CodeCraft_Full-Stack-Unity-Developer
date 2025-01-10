using System;
using Game.Views;
using Modules.Money;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly MoneyView _view;

        public MoneyPresenter(
            IMoneyStorage moneyStorage, 
            MoneyView view)
        {
            _moneyStorage = moneyStorage;
            _view = view;
        }

        void IInitializable.Initialize()
        {
            _view.SetMoneyText(ConvertMoneyText(_moneyStorage.Money));
            
            _moneyStorage.OnMoneySpent += UpdateMoney;
            _moneyStorage.OnMoneyEarned += UpdateMoney;
            _moneyStorage.OnMoneyChanged += UpdateMoney;
        }

        void IDisposable.Dispose()
        {
            _moneyStorage.OnMoneySpent -= UpdateMoney;
            _moneyStorage.OnMoneyEarned -= UpdateMoney;
            _moneyStorage.OnMoneyChanged -= UpdateMoney;
        }

        public void LockChanging() => _view.LockChanging();
        
        public void UpdateMoneyWithAnimation(int earnings)
        {
            var endValue = _moneyStorage.Money;
            var startValue = endValue - earnings;
            _view.ChangeMoneyWithAnimation(startValue, endValue, ConvertMoneyText);
        }

        private void UpdateMoney(int newvalue, int prevvalue) => _view.SetMoneyText(ConvertMoneyText(newvalue));

        private string ConvertMoneyText(int value) => value.ToString();
    }
}