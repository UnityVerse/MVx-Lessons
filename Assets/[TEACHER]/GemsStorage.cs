using System;
using Sirenix.OdinInspector;

namespace SampleGame
{
    public sealed class GemsStorage
    {
        public event Action<short> OnGemsChanged;

        public short Gems
        {
            get { return this.gems; }
        }

        [ReadOnly]
        [ShowInInspector]
        private short gems;

        [Button]
        public void SetupGems(short gems)
        {
            this.gems = gems;
        }

        [Button]
        public void AddGems(short range)
        {
            this.gems += range;
            this.OnGemsChanged?.Invoke(this.gems);
        }

        [Button]
        public void SpendGems(short range)
        {
            this.gems -= range;
            this.OnGemsChanged?.Invoke(this.gems);
        }
    }
}