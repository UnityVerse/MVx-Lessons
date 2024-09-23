using System;
using UnityEngine;

namespace SampleGame
{
    public interface IProductPresenter
    {
        event Action<bool> OnBuyButtonInteractible;
        
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
        string Price { get; }
        
        bool IsBuyButtonInteractible { get; }
        
        void OnBuyClick();
    }
}