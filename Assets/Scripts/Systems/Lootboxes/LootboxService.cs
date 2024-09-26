using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class LootboxService : ILootboxService, IFixedTickable
    {
        public event Action<Lootbox> OnLootboxAdded;
        public event Action<Lootbox> OnLootboxRemoved;

        [ShowInInspector]
        public IReadOnlyList<Lootbox> Lootboxes => this.lootboxes;

        private readonly List<Lootbox> lootboxes;

        public LootboxService(IEnumerable<Lootbox> lootboxes)
        {
            this.lootboxes = new List<Lootbox>(lootboxes);
        }

        [Button]
        public void AddLootbox(Lootbox lootbox)
        {
            if (!this.lootboxes.Contains(lootbox))
            {
                this.lootboxes.Add(lootbox);
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
                this.OnLootboxRemoved?.Invoke(lootbox);
            }
        }

        void IFixedTickable.FixedTick()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = this.lootboxes.Count; i < count; i++)
            {
                this.lootboxes[i].Tick(deltaTime);
            }
        }
    }
}