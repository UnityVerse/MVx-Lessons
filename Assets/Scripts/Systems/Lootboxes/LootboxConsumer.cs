namespace SampleGame
{
    public sealed class LootboxConsumer
    {
        private readonly CurrencyBank _currencyBank;

        public LootboxConsumer(CurrencyBank currencyBank)
        {
            _currencyBank = currencyBank;
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
    }
}