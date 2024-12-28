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
            _view.SetMoneyText(_moneyStorage.Money.ToString());
            
            _moneyStorage.OnMoneyChanged += UpdateMoney;
        }

        void IDisposable.Dispose()
        {
            _moneyStorage.OnMoneyChanged -= UpdateMoney;
        }
        
        private void UpdateMoney(int newvalue, int prevvalue)
        {
            _view.SetMoneyText(newvalue.ToString());
        }
    }
}