// using GameSystem;
// using UnityEngine;
//
// namespace Lessons.Architecture.MVO
// {
//     public sealed class MoneyPanelAdapter : MonoBehaviour, IGameInitElement, IGameFinishElement
//     {
//         [SerializeField]
//         private CurrencyPanel panel;
//
//         private MoneyStorage moneyStorage; //Бизнес-логика
//
//         [GameInject]
//         public void Construct(MoneyStorage moneyStorage)
//         {
//             this.moneyStorage = moneyStorage;
//         }
//
//         void IGameInitElement.InitGame()
//         {
//             this.panel.SetupCurrency(this.moneyStorage.Money.ToString());
//             this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
//         }
//
//         void IGameFinishElement.FinishGame()
//         {
//             this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
//         }
//
//         private void OnMoneyChanged(int money)
//         {
//             this.panel.UpdateCurrency(money.ToString());
//         }
//     }
// }