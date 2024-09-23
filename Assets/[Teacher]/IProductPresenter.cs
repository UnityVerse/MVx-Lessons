// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public interface IProductPresenter
//     {
//         event Action OnStateChanged;
//         event Action<bool> OnBuyButtonInteractible;
//         
//         string Title { get; }
//         string Description { get; }
//         Sprite Icon { get; }
//         IReadOnlyList<IPriceElement> PriceElements { get; }
//         bool IsBuyButtonInteractible { get; }
//         
//         void OnBuyClick();
//         
//         public interface IPriceElement
//         {
//             string Price { get; }
//             Sprite Icon { get; }
//         }
//     }
// }