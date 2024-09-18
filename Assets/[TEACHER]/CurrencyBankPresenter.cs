// using System;
// using System.Collections.Generic;
// using Zenject;
//
// namespace SampleGame
// {
//     public sealed class CurrencyBankPresenter : IInitializable, IDisposable
//     {
//         private readonly CurrencyBank currencyBank;
//         private readonly CurrencyListView listView;
//
//         private readonly List<CurrencyPresenter> _presenters = new();
//
//         public CurrencyBankPresenter(CurrencyBank currencyBank, CurrencyListView listView)
//         {
//             this.currencyBank = currencyBank;
//             this.listView = listView;
//         }
//
//         public void Initialize()
//         {
//             foreach (CurrencyCell cell in this.currencyBank)
//             {
//                 CurrencyView currencyView = this.listView.SpawnView();
//                 CurrencyPresenter presenter = new CurrencyPresenter(currencyView, cell);
//                 presenter.Initialize();
//                 
//                 _presenters.Add(presenter);
//             }
//         }
//
//         public void Dispose()
//         {
//             foreach (var presenter in _presenters)
//             {
//                 this.listView.UnspawnView(presenter.View);
//                 presenter.Dispose();
//             }
//         }
//     }
// }