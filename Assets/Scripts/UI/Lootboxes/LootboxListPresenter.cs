using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxListPresenter : IInitializable, IDisposable
    {
        private readonly Lootbox[] _lootboxes;
        private readonly LootboxListView _listView;
        private readonly LootboxConsumer _lootboxConsumer;
        private readonly CurrencyBank _currencyBank;

        private readonly List<LootboxPresenter> _presenters = new();

        public LootboxListPresenter(
            Lootbox[] lootboxes,
            LootboxListView listView,
            LootboxConsumer consumer,
            CurrencyBank currencyBank
        )
        {
            _lootboxes = lootboxes;
            _listView = listView;
            _lootboxConsumer = consumer;
            _currencyBank = currencyBank;
        }

        public void Initialize()
        {
            for (int i = 0, count = _lootboxes.Length; i < count; i++)
            {
                Lootbox lootbox = _lootboxes[i];
                LootboxView view = _listView.SpawnElement();
                LootboxPresenter presenter = new LootboxPresenter(lootbox, view, _lootboxConsumer, _currencyBank);
                presenter.Initialize();
                
                _presenters.Add(presenter);
            }
        }

        public void Dispose()
        {
            for (int i = 0, count = _presenters.Count; i < count; i++)
            {
                LootboxPresenter presenter = _presenters[i];
                presenter.Dispose();
            }
            
            _listView.Clear();
        }
    }
}