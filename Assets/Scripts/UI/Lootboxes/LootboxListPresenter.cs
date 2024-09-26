using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxListPresenter : IInitializable, IDisposable
    {
        private readonly Lootbox[] _lootboxes;
        private readonly LootboxListView _listView;
        private readonly LootboxPresenter.Factory _factory;

        private readonly List<LootboxPresenter> _presenters = new();

        public LootboxListPresenter(
            Lootbox[] lootboxes,
            LootboxListView listView,
            LootboxPresenter.Factory factory
        )
        {
            _lootboxes = lootboxes;
            _listView = listView;
            _factory = factory;
        }

        public void Initialize()
        {
            for (int i = 0, count = _lootboxes.Length; i < count; i++)
            {
                Lootbox lootbox = _lootboxes[i];
                LootboxView view = _listView.SpawnElement();
                LootboxPresenter presenter = _factory.Create(lootbox, view);
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