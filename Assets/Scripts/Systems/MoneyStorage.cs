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

        [ShowInInspector, ReadOnly]
        public int Money { get; private set; }

        public MoneyStorage(int money)
        {
            Money = money;
        }

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
        }

        [Button]
        public void AddMoney(int range)
        {
            this.Money += range;
            this.OnMoneyAdded?.Invoke(this.Money, range);
        }

        [Button]
        public void SpendMoney(int range)
        {
            this.Money -= range;
            this.OnMoneyRemoved?.Invoke(this.Money, range);
        }
    }
}