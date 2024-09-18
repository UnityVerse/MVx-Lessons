// using System;
// using Zenject;
//
// namespace SampleGame
// {
//     public sealed class CurrencyPresenter : IInitializable, IDisposable
//     {
//         public CurrencyView View => this.view;
//         public CurrencyCell Cell => this.cell;
//         
//         private readonly CurrencyView view;
//         private readonly CurrencyCell cell;
//
//         public CurrencyPresenter(CurrencyView view, CurrencyCell cell)
//         {
//             this.view = view;
//             this.cell = cell;
//         }
//
//         public void Initialize()
//         {
//             this.view.SetCurrency(this.cell.Amount.ToString());
//             this.view.SetIcon(this.cell.Icon);
//
//             this.cell.OnAmountChanged += this.OnMoneyChanged;
//             this.cell.OnAmountAdded += this.OnMoneyAdded;
//             this.cell.OnAmountSpent += this.OnMoneyRemoved;
//         }
//
//         public void Dispose()
//         {
//             this.cell.OnAmountChanged -= this.OnMoneyChanged;
//             this.cell.OnAmountAdded -= this.OnMoneyAdded;
//             this.cell.OnAmountSpent -= this.OnMoneyRemoved;
//         }
//         
//         private void OnMoneyChanged(int newValue)
//         {
//             this.view.ChangeCurrency(newValue.ToString());
//         }
//
//         private void OnMoneyAdded(int range)
//         {
//             this.view.EarnCurrency(this.cell.Amount, range);
//         }
//
//         private void OnMoneyRemoved(int _)
//         {
//             this.view.SpendCurrency(this.cell.Amount.ToString());
//         }
//     }
// }