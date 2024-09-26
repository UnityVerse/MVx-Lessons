using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxManager : IFixedTickable
    {
        [ShowInInspector, ReadOnly]
        public IReadOnlyList<Lootbox> Lootboxes => _lootboxes;
        
        private readonly CurrencyBank _currencyBank;
        private readonly IReadOnlyList<Lootbox> _lootboxes;

        public LootboxManager(CurrencyBank currencyBank, IReadOnlyList<Lootbox> lootboxes)
        {
            _currencyBank = currencyBank;
            _lootboxes = lootboxes;
        }

        public bool CanConsume(Lootbox lootbox)
        {
            return lootbox.IsReady;
        }

        [Button]
        public bool Consume(int lootboxIndex)
        {
            return lootboxIndex < 0 || lootboxIndex >= _lootboxes.Count
                ? throw new ArgumentOutOfRangeException(nameof(lootboxIndex))
                : this.Consume(_lootboxes[lootboxIndex]);
        }
        
        public bool Consume(Lootbox lootbox)
        {
            if (lootbox.Consume())
            {
                _currencyBank.Add(lootbox.CurrencyReward);
                return true;
            }

            return false;
        }

        void IFixedTickable.FixedTick()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = _lootboxes.Count; i < count; i++)
            {
                Lootbox lootbox = _lootboxes[i];
                lootbox.TickTimer(deltaTime);
            }
        }
    }
}