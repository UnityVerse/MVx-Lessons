// using Sirenix.OdinInspector;
//
// namespace SampleGame
// {
//     public sealed class MoneyStorage
//     {
//         public delegate void AddedDelegate(int newValue, int range);
//         public delegate void RemovedDelegate(int newValue, int range);
//         public delegate void ChangedDelegate(int newValue, int prevValue); 
//         
//         public event AddedDelegate OnMoneyEarned; 
//         public event RemovedDelegate OnMoneySpent;
//         public event ChangedDelegate OnMoneyChanged; 
//
//         [ShowInInspector, ReadOnly]
//         public int Money { get; private set; }
//
//         public MoneyStorage(int money)
//         {
//             Money = money;
//         }
//
//         public void SetupMoney(int money)
//         {
//             this.Money = money;
//         }
//         
//         [Button]
//         public void ChangeMoney(int money)
//         {
//             int previousMoney = this.Money;
//             this.Money = money;
//             this.OnMoneyChanged?.Invoke(money, previousMoney);
//         }
//
//         [Button]
//         public void EarnMoney(int range)
//         {
//             this.Money += range;
//             this.OnMoneyEarned?.Invoke(this.Money, range);
//         }
//
//         [Button]
//         public void SpendMoney(int range)
//         {
//             this.Money -= range;
//             this.OnMoneySpent?.Invoke(this.Money, range);
//         }
//     }
// }