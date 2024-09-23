using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    public sealed class ProductBuyer
    {
        public event Action<Product> OnProductBought; 

        private readonly MoneyStorage moneyStorage;

        public ProductBuyer(MoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }
        
        [Button]
        public bool CanBuy(Product product)
        {
            return this.moneyStorage.Money >= product.price;
        }

        [Button]
        public bool Buy(Product product)
        {
            if (!this.CanBuy(product))
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.title}!</color>");
                return false;
            }

            this.moneyStorage.SpendMoney(product.price);
            this.OnProductBought?.Invoke(product);
            Debug.Log($"<color=green>Product {product.title} successfully purchased!</color>");
            return true;
        }
    }
}