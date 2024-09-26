using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxListPresenter : IInitializable, IDisposable
    {
        private readonly ILootboxService _lootboxService; //Model:
        private readonly LootboxListView _listView; //View:

        private readonly LootboxPresenter.Factory _factory; //Presenter: 
        private readonly Dictionary<Lootbox, LootboxPresenter> _presenters = new();

        public LootboxListPresenter(
            ILootboxService lootboxService,
            LootboxListView listView,
            LootboxPresenter.Factory factory
        )
        {
            _lootboxService = lootboxService;
            _factory = factory;
            _listView = listView;
        }

        public void Initialize()
        {
            _lootboxService.OnLootboxAdded += this.AddLootbox;
            _lootboxService.OnLootboxRemoved += this.OnLootboxRemoved;
            
            IReadOnlyList<Lootbox> lootboxes = _lootboxService.Lootboxes;
            for (int i = 0, count = lootboxes.Count; i < count; i++)
            {
                Lootbox lootbox = lootboxes[i];
                this.AddLootbox(lootbox);
            }
        }

        private void AddLootbox(Lootbox lootbox)
        {
            if (!_presenters.ContainsKey(lootbox))
            {
                LootboxView view = _listView.SpawnElement();
                LootboxPresenter presenter = _factory.Create(lootbox, view);
                presenter.Initialize();
                _presenters.Add(lootbox, presenter);
            }
        }

        private void OnLootboxRemoved(Lootbox lootbox)
        {
            if (_presenters.Remove(lootbox, out LootboxPresenter presenter))
            {
                presenter.Dispose();
                _listView.DespawnElement(presenter.View);
            }
        }

        public void Dispose()
        {
            _lootboxService.OnLootboxAdded -= this.AddLootbox;
            _lootboxService.OnLootboxRemoved -= this.OnLootboxRemoved;

            foreach (LootboxPresenter presenter in _presenters.Values)
            {
                presenter.Dispose();
            }

            _presenters.Clear();
            _listView.Clear();
        }
    }
}