using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    public sealed class ProductBuyer
    {
        public event Action<Product> OnProductBought; 

        private readonly CurrencyBank _currencyBank;

        public ProductBuyer(CurrencyBank bank)
        {
            _currencyBank = bank;
        }
        
        [Button]
        public bool CanBuy(Product product)
        {
            return _currencyBank.IsEnough(product.Price);
        }

        [Button]
        public bool Buy(Product product)
        {
            if (!this.CanBuy(product))
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.Title}!</color>");
                return false;
            }

            _currencyBank.Spend(product.Price);
            this.OnProductBought?.Invoke(product);
            Debug.Log($"<color=green>Product {product.Title} successfully purchased!</color>");
            return true;
        }
    }
}