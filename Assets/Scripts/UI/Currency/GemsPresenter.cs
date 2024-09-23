using System;
using Zenject;

namespace SampleGame
{
    public sealed class GemsPresenter : IInitializable, IDisposable
    {
        private readonly GemsStorage model;
        private readonly CurrencyView view;

        private short _currentGems;

        public GemsPresenter(GemsStorage model, CurrencyView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            _currentGems = this.model.Gems;
            this.view.Setup(_currentGems.ToString());

            this.model.OnGemsChanged += this.OnGemsChanged;
        }

        public void Dispose()
        {
            this.model.OnGemsChanged -= this.OnGemsChanged;
        }

        private void OnGemsChanged(short gems)
        {
            int diff = gems - _currentGems;

            if (diff > 0) //Add
            {
                this.view.SetCurrencyAsAdded(_currentGems, diff);
            }
            else if (diff < 0) //Remove
            {
                this.view.SetCurrencyAsRemoved(gems.ToString());
            }

            _currentGems = gems;
        }
    }
}