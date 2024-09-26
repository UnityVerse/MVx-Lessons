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
            
            _lootbox.OnTimerTicked += this.OnTimerTicked;
            _lootbox.OnReady += this.OnReady;

            _view.OnCollectClicked += this.OnCollectClicked;
        }

        public void Dispose()
        {
            _lootbox.OnTimerTicked -= this.OnTimerTicked;
            _lootbox.OnReady -= this.OnReady;
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

        private void OnTimerTicked(Lootbox lootbox, float remainingTime)
        {
            this.UpdateRemainingTime(remainingTime);
        }

        private void OnReady(Lootbox _, bool ready)
        {
            _view.SetReady(ready);
        }

        private void OnCollectClicked()
        {
            _lootboxConsumer.Consume(_lootbox);
        }
    }
}