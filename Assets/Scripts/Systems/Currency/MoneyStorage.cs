using System;
using Sirenix.OdinInspector;

namespace SampleGame
{
    public sealed class MoneyStorage
    {
        public delegate void AddedDelegate(int newValue, int range);
        public delegate void RemovedDelegate(int newValue, int range);
        public delegate void ChangedDelegate(int newValue, int previousValue); 
        
        public event AddedDelegate OnMoneyAdded; 
        public event RemovedDelegate OnMoneyRemoved;
        public event ChangedDelegate OnMoneyChanged;
        public event Action<int> OnStateChanged;

        [ShowInInspector, ReadOnly]
        public int Money { get; private set; }
        
        public void SetupMoney(int money)
        {
            this.Money = money;
        }
        
        [Button]
        public void ChangeMoney(int money)
        {
            int previousMoney = this.Money;
            this.Money = money;
            this.OnMoneyChanged?.Invoke(money, previousMoney);
            this.OnStateChanged?.Invoke(this.Money);
        }

        [Button]
        public void AddMoney(int range)
        {
            this.Money += range;
            this.OnMoneyAdded?.Invoke(this.Money, range);
            this.OnStateChanged?.Invoke(this.Money);
        }

        [Button]
        public void SpendMoney(int range)
        {
            this.Money -= range;
            this.OnMoneyRemoved?.Invoke(this.Money, range);
            this.OnStateChanged?.Invoke(this.Money);
        }
    }
}