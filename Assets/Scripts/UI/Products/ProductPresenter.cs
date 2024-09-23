using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using static SampleGame.IProductPresenter;

namespace SampleGame
{
    public sealed class ProductPresenter : IProductPresenter, IInitializable, ILateTickable
    {
        private const float SYNC_PERIOD = 0.2f;
        
        public event Action OnStateChanged;
        public event Action<bool> OnBuyButtonEnabled;

        [ShowInInspector]
        public string Title => _currentProduct ? _currentProduct.Title : string.Empty;

        [ShowInInspector]
        public string Description => _currentProduct ? _currentProduct.Description : string.Empty;

        [ShowInInspector]
        public Sprite Icon => _currentProduct ? _currentProduct.Icon : null;

        [ShowInInspector]
        public string Price => _currentProduct ? _currentProduct.Price[0].amount.ToString() : string.Empty;

        [ShowInInspector]
        public bool IsBuyButtonEnabled => this.buttonBuyEnabled;

        public IReadOnlyList<IPriceElement> PriceElements => this.priceElements;

        private readonly ProductBuyer _productBuyer;
        private readonly CurrencyBank _currencyBank;

        [ShowInInspector]
        private Product _currentProduct;

        private readonly List<IPriceElement> priceElements = new();
        private bool buttonBuyEnabled;
        private float syncTime;

        public ProductPresenter(ProductBuyer productBuyer, CurrencyBank bank, Product currentProduct = default)
        {
            _productBuyer = productBuyer;
            _currencyBank = bank;
            this.ChangeProduct(currentProduct);
        }

        [Button]
        public void ChangeProduct(Product product)
        {
            if (product == _currentProduct)
            {
                return;
            }

            _currentProduct = product;
            this.buttonBuyEnabled = this.CanBuy();
            this.UpdatePrice(product);
            this.OnStateChanged?.Invoke();
        }
        
        public void Initialize()
        {
            this.OnCurrencyChanged();
        }

        public void LateTick()
        {
            this.syncTime += Time.deltaTime;
            
            if (this.syncTime > SYNC_PERIOD)
            {
                this.OnCurrencyChanged();
                this.syncTime -= SYNC_PERIOD;
            }
        }

        private void UpdatePrice(Product product)
        {
            this.priceElements.Clear();

            if (product == null)
            {
                return;
            }

            int count = product.Price.Count;
            for (int i = 0; i < count; i++)
            {
                this.priceElements.Add(new PriceElement(this, i));
            }
        }

        private void OnCurrencyChanged()
        {
            bool canBuy = this.CanBuy();
            if (canBuy != this.buttonBuyEnabled)
            {
                this.buttonBuyEnabled = canBuy;
                this.OnBuyButtonEnabled?.Invoke(this.IsBuyButtonEnabled);
            }
        }

        private bool CanBuy()
        {
            return _currentProduct && _productBuyer.CanBuy(_currentProduct);
        }

        public void OnBuyClicked()
        {
            if (_currentProduct && _productBuyer.Buy(_currentProduct))
            {
                this.OnCurrencyChanged();
            }
        }

        private sealed class PriceElement : IPriceElement
        {
            public string Price => this.GetPriceItem().amount.ToString();
            public Sprite Icon => _parent._currencyBank.GetCell(this.GetPriceItem().type).Icon;

            private readonly ProductPresenter _parent;
            private readonly int _priceIndex;

            public PriceElement(ProductPresenter parent, int priceIndex)
            {
                _parent = parent;
                _priceIndex = priceIndex;
            }
            
            private CurrencyData GetPriceItem() => _parent._currentProduct!.Price[_priceIndex];
        }
    }
}