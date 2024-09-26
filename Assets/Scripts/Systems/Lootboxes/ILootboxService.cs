using System;
using System.Collections.Generic;

namespace SampleGame
{
    public interface ILootboxService
    {
        event Action<Lootbox> OnLootboxAdded;
        event Action<Lootbox> OnLootboxRemoved;

        IReadOnlyList<Lootbox> Lootboxes { get; }

        void AddLootbox(Lootbox lootbox);
        void RemoveLootbox(Lootbox lootbox);
    }
}