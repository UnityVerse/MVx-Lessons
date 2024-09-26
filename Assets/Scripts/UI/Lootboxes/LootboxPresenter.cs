using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxPresenter : IInitializable, IDisposable
    {
        public LootboxView View => _view;

        private readonly Lootbox _lootbox; //Model
        private readonly LootboxConsumer _lootboxConsumer; //Model
        private readonly CurrencyBank _currencyBank;

        private readonly LootboxView _view; //Passive-View

        public LootboxPresenter(
            Lootbox lootbox,
            LootboxView view,
            LootboxConsumer lootboxConsumer,
            CurrencyBank currencyBank
        )
        {
            _lootbox = lootbox;
            _view = view;
            _lootboxConsumer = lootboxConsumer;
            _currencyBank = currencyBank;
        }


        //Start
        public void Initialize()
        {
            _view.SetIcon(_lootbox.Icon);
            this.UpdateResourceElements();
            this.UpdateRemainingTime(_lootbox.RemainingTime);
            this.UpdateReadyState(_lootbox.IsReady);

            _view.OnCollectClicked += this.OnCollectClicked;
            _lootbox.OnReady += this.UpdateReadyState;
            _lootbox.OnTimeChanged += this.UpdateRemainingTime;
        }

        private void UpdateResourceElements()
        {
            IReadOnlyList<CurrencyData> resources = _lootbox.CurrencyReward;
            for (int i = 0, count = resources.Count; i < count; i++)
            {
                CurrencyData resource = resources[i];
                AmountView resourceView = _view.SpawnResourceElement();
                resourceView.SetAmount(resource.amount.ToString());
                resourceView.SetIcon(_currencyBank.GetCell(resource.type).Icon);
            }
        }

        private void UpdateRemainingTime(float remainingTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            _view.SetRemainingTime($"{timeSpan.Minutes}m:{timeSpan.Seconds}s");
        }

        private void UpdateReadyState(bool isReady)
        {
            _view.SetReady(isReady);
        }

        //OnDestroy
        public void Dispose()
        {
            _lootbox.OnReady -= UpdateReadyState;
            _lootbox.OnTimeChanged -= this.UpdateRemainingTime;
            _view.OnCollectClicked -= this.OnCollectClicked;
            _view.ClearResourceElements();
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