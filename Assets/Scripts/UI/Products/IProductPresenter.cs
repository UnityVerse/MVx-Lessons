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

        void OnBuyClicked();
        void ChangeProduct(Product product);
        
        public interface IPriceElement
        {
            string Price { get; }
            Sprite Icon { get; }
        }
    }
}