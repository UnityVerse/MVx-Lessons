using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    public sealed class ProductBuyer
    {
        public event Action<Product> OnProductBought; 

        private readonly CurrencyCell _moneyStorage;

        public ProductBuyer(CurrencyBank bank)
        {
            _moneyStorage = bank.GetCell(CurrencyType.MONEY);
        }
        
        [Button]
        public bool CanBuy(Product product)
        {
            return _moneyStorage.Amount >= product.price;
        }

        [Button]
        public bool Buy(Product product)
        {
            if (!this.CanBuy(product))
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.title}!</color>");
                return false;
            }

            _moneyStorage.Spend(product.price);
            this.OnProductBought?.Invoke(product);
            Debug.Log($"<color=green>Product {product.title} successfully purchased!</color>");
            return true;
        }
    }
}