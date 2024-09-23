using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class CurrencyCell
    {
        public event Action OnStateChanged;
        public event Action<int> OnAmountChanged;
        public event Action<int> OnAmountAdded;
        public event Action<int> OnAmountSpent;

        public CurrencyType Type => _type;
        public int Amount => _amount;
        public Sprite Icon => _icon;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private CurrencyType _type;

        [SerializeField]
        private int _amount;

        public CurrencyCell(CurrencyType type, int amount = 0)
        {
            _type = type;
            _amount = amount;
        }

        [Button]
        public bool Add(int range)
        {
            if (range <= 0)
            {
                return false;
            }

            _amount += range;
            this.OnAmountAdded?.Invoke(range);
            this.OnStateChanged?.Invoke();
            return true;
        }

        [Button]
        public bool Spend(int range)
        {
            if (range <= 0)
            {
                return false;
            }

            if (_amount < range)
            {
                return false;
            }

            _amount -= range;
            this.OnAmountSpent?.Invoke(range);
            this.OnStateChanged?.Invoke();
            return true;
        }

        [Button]
        public void Change(int amount)
        {
            if (_amount != amount)
            {
                _amount = amount;
                this.OnAmountChanged?.Invoke(_amount);
                this.OnStateChanged?.Invoke();
            }
        }

        public bool IsEnough(int range)
        {
            return _amount >= range;
        }
    }
}