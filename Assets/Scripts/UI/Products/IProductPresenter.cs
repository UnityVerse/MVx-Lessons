using System;
using System.Collections.Generic;
using UnityEngine;

namespace SampleGame
{
    public interface IProductPresenter
    {
        event Action OnStateChanged;
        event Action<bool> OnBuyButtonEnabled;
        
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
        bool IsBuyButtonEnabled { get; }
        IReadOnlyList<IPriceElement> PriceElements { get; }
        
        void SetProduct(Product product);
        void OnBuyClicked();
        
        public interface IPriceElement
        {
            string Amount { get; }
            Sprite Icon { get; }
        }
    }
}