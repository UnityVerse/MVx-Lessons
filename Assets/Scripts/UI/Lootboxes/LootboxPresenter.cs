using System;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxPresenter : IInitializable, IDisposable
    {
        private readonly Lootbox _lootbox;
        private readonly LootboxView _view;
        private readonly CurrencyBank _currencyBank;
        private readonly LootboxConsumer _lootboxConsumer;

        public LootboxView View => _view;
        public Lootbox Model => _lootbox;

        public LootboxPresenter(Lootbox lootbox, LootboxView view, LootboxConsumer lootboxConsumer, CurrencyBank currencyBank)
        {
            _lootbox = lootbox;
            _view = view;
            _currencyBank = currencyBank;
            _lootboxConsumer = lootboxConsumer;
        }

        public void Initialize()
        {
            _view.SetIcon(_lootbox.Icon);
            _view.SetReady(_lootbox.IsReady);
            
            this.UpdatePrice();
            this.UpdateRemainingTime(_lootbox.RemainingTime);
            
            _lootbox.OnTickTimer += this.UpdateRemainingTime;
            _lootbox.OnReady += this.UpdateReadyState;

            _view.OnCollectClicked += this.OnCollectClicked;
        }

        public void Dispose()
        {
            _lootbox.OnTickTimer -= this.UpdateRemainingTime;
            _lootbox.OnReady -= this.UpdateReadyState;
        }

        private void UpdatePrice()
        {
            _view.ClearPriceElements();
            
            foreach (CurrencyData currency in _lootbox.CurrencyReward)
            {
                PriceView priceView = _view.SpawnPriceElement();
                priceView.SetIcon(_currencyBank.GetCell(currency.type).Icon);
                priceView.SetAmount(currency.amount.ToString());
            }
        }

        private void UpdateRemainingTime(float remainingTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            _view.SetRemainingTime($"{timeSpan.Minutes}m:{timeSpan.Seconds}s");
        }

        private void UpdateReadyState(bool ready)
        {
            _view.SetReady(ready);
        }

        private void OnCollectClicked()
        {
            _lootboxConsumer.Consume(_lootbox);
        }
        
        public sealed class Factory : PlaceholderFactory<Lootbox, LootboxView, LootboxPresenter>
        {
        }
    }
}