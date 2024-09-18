// using System;
// using Zenject;
//
// namespace SampleGame
// {
//     //Zenject
//     public sealed class MoneyPresenter : IInitializable, IDisposable
//     {
//         private readonly MoneyStorage moneyStorage;
//         private readonly CurrencyView moneyView;
//
//         public MoneyPresenter(MoneyStorage moneyStorage, CurrencyView moneyView)
//         {
//             this.moneyStorage = moneyStorage;
//             this.moneyView = moneyView;
//         }
//         
//         //Start
//         public void Initialize()
//         {
//             this.moneyView.SetupCurrency(this.moneyStorage.Money.ToString());
//
//             this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
//             this.moneyStorage.OnMoneyEarned += this.OnMoneyAdded;
//             this.moneyStorage.OnMoneySpent += this.OnMoneyRemoved;
//         }
//
//         //OnDestroy
//         public void Dispose()
//         {
//             this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
//             this.moneyStorage.OnMoneyEarned -= this.OnMoneyAdded;
//             this.moneyStorage.OnMoneySpent -= this.OnMoneyRemoved;
//         }
//
//         private void OnMoneyChanged(int newvalue, int prevvalue)
//         {
//             this.moneyView.ChangeCurrency(newvalue.ToString());
//         }
//
//         private void OnMoneyRemoved(int newvalue, int range)
//         {
//             this.moneyView.RemoveCurrency(newvalue.ToString());
//         }
//
//         private void OnMoneyAdded(int newvalue, int range)
//         {
//             this.moneyView.AddCurrency(newvalue, range);
//         }
//     }
// }