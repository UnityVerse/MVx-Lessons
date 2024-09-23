using System;
using Zenject;

namespace SampleGame
{
    public sealed class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly MoneyStorage model;
        private readonly CurrencyView view;

        public MoneyPresenter(MoneyStorage model, CurrencyView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            this.view.Setup(this.model.Money.ToString());
            
            this.model.OnMoneyChanged += this.OnMoneyChanged;
            this.model.OnMoneyAdded += this.OnMoneyAdded;
            this.model.OnMoneyRemoved += this.OnMoneyRemoved;
        }

        public void Dispose()
        {
            this.model.OnMoneyChanged -= this.OnMoneyChanged;
            this.model.OnMoneyAdded -= this.OnMoneyAdded;
            this.model.OnMoneyRemoved -= this.OnMoneyRemoved;
        }

        private void OnMoneyChanged(int newValue, int previousValue)
        {
            this.view.SetCurrencyAsChanged(newValue.ToString());
        }

        private void OnMoneyRemoved(int newValue, int range)
        {
            this.view.SetCurrencyAsRemoved(newValue.ToString());
        }

        private void OnMoneyAdded(int newValue, int range)
        {
            int startMoney = newValue - range;
            this.view.SetCurrencyAsAdded(startMoney, range);
        }
    }
}