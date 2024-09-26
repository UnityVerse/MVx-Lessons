using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxService : ILootboxService
    {
        public event Action<Lootbox> OnLootboxAdded;
        public event Action<Lootbox> OnLootboxRemoved;

        [ShowInInspector]
        public IReadOnlyList<Lootbox> Lootboxes => this.lootboxes;

        private readonly List<Lootbox> lootboxes;
        private readonly TickableManager _tickableManager;

        public LootboxService(IEnumerable<Lootbox> lootboxes, TickableManager tickableManager)
        {
            this.lootboxes = new List<Lootbox>(lootboxes);
            _tickableManager = tickableManager;

            foreach (var lootbox in this.lootboxes)
            {
                _tickableManager.AddFixed(lootbox);
            }
        }

        [Button]
        public void AddLootbox(Lootbox lootbox)
        {
            if (!this.lootboxes.Contains(lootbox))
            {
                this.lootboxes.Add(lootbox);
                _tickableManager.AddFixed(lootbox);
                this.OnLootboxAdded?.Invoke(lootbox);
            }
        }

        [Button]
        public void RemoveLootboxAt(int index)
        {
            if (index < 0 || index >= this.lootboxes.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            
            this.RemoveLootbox(this.lootboxes[index]);
        }

        public void RemoveLootbox(Lootbox lootbox)
        {
            if (this.lootboxes.Remove(lootbox))
            {
                _tickableManager.RemoveFixed(lootbox);
                this.OnLootboxRemoved?.Invoke(lootbox);
            }
        }
    }
}