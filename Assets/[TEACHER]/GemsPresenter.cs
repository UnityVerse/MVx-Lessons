// using System;
// using Zenject;
//
// namespace SampleGame
// {
//     public sealed class GemsPresenter : IInitializable, IDisposable
//     {
//         private readonly GemsStorage gemsStorage;
//         private readonly CurrencyView currencyView;
//
//         private int _previousGems;
//         
//         public GemsPresenter(GemsStorage gemsStorage, CurrencyView currencyView)
//         {
//             this.gemsStorage = gemsStorage;
//             this.currencyView = currencyView;
//         }
//
//         public void Initialize()
//         {
//             _previousGems = this.gemsStorage.Gems;
//             this.currencyView.SetupCurrency(_previousGems.ToString());
//             this.gemsStorage.OnGemsChanged += this.OnGemsChanged;
//         }
//
//         public void Dispose()
//         {
//             this.gemsStorage.OnGemsChanged -= this.OnGemsChanged;
//         }
//
//         private void OnGemsChanged(int gems)
//         {
//             int diff = gems - _previousGems;
//
//             if (diff == 0)
//             {
//                 return;
//             }
//
//             if (diff > 0)
//             {
//                 this.currencyView.AddCurrency(gems, diff);
//             }
//             else
//             {
//                 this.currencyView.RemoveCurrency(gems.ToString());
//             }
//
//             _previousGems = gems;
//         }
//     }
// }