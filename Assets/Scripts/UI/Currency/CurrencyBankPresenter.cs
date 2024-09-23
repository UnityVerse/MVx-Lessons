using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class CurrencyBankPresenter : IInitializable, IDisposable
    {
        private readonly CurrencyBank bank;
        private readonly CurrencyListView listView;

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
                CurrencyView cellView = this.listView.SpawnItem();
                CurrencyPresenter itemPresenter = new CurrencyPresenter(cell, cellView);
                this.presenters.Add(itemPresenter);
            }
        }

        public void Dispose()
        {
            foreach (CurrencyPresenter presenter in this.presenters)
            {
                presenter.Dispose();
                this.listView.UnspawnItem(presenter.View);
            }
        }
    }
}