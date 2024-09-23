using System;

namespace SampleGame
{
    [Serializable]
    public struct CurrencyData
    {
        public CurrencyType type;
        public int amount;
    }
}