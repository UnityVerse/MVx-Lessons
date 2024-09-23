using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace SampleGame
{
    public sealed class CurrencyBank : IEnumerable<CurrencyCell>
    {
        [ShowInInspector]
        private readonly Dictionary<CurrencyType, CurrencyCell> _cells;

        public CurrencyBank(IEnumerable<CurrencyCell> cells)
        {
            _cells = cells.ToDictionary(it => it.Type);
        }

        public CurrencyCell GetCell(in CurrencyType type)
        {
            return _cells[type];
        }

        public bool IsEnough(in CurrencyData[] range)
        {
            for (int i = 0, count = range.Length; i < count; i++)
            {
                CurrencyData currency = range[i];
                if (!_cells.TryGetValue(currency.type, out CurrencyCell cell))
                {
                    throw new ArgumentException($"Currency type {currency.type} is not found!");
                }
                
                if (!cell.IsEnough(currency.amount))
                {
                    return false;
                }
            }
            
            return true;
        }

        public bool Spend(in CurrencyData[] range)
        {
            if (!this.IsEnough(range))
            {
                return false;
            }
            
            for (int i = 0, count = range.Length; i < count; i++)
            {
                CurrencyData currency = range[i];
                CurrencyCell cell = _cells[currency.type];
                cell.Spend(currency.amount);
            }
            
            return true;
        }

        public IEnumerator<CurrencyCell> GetEnumerator()
        {
            return _cells.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}