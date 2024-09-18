using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class CurrencyBankPresenter : IInitializable, IDisposable, IEnumerable<CurrencyPresenter>
    {
        private readonly CurrencyBank bank;
        private readonly CurrencyListView listView;

        public IReadOnlyList<CurrencyPresenter> Presenters => this.presenters;

        private readonly List<CurrencyPresenter> presenters = new();
        
        public CurrencyBankPresenter(CurrencyBank bank, CurrencyListView listView)
        {
            this.bank = bank;
            this.listView = listView;
        }

        public void Initialize()
        {
            foreach (CurrencyCell cell in this.bank)
            {
                ICurrencyView cellView = this.listView.SpawnItem();
                CurrencyPresenter itemPresenter = new CurrencyPresenter(cell, cellView);
                this.presenters.Add(itemPresenter);
            }
        }

        public void Dispose()
        {
            foreach (var presenter in this.presenters)
            {
                presenter.Dispose();
                this.listView.UnspawnItem(presenter.View);
            }
        }

        public IEnumerator<CurrencyPresenter> GetEnumerator()
        {
            return this.presenters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}