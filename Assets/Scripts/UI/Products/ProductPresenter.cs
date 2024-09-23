using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using static SampleGame.IProductPresenter;

namespace SampleGame
{
    public sealed class ProductPresenter : IInitializable, ILateTickable, IProductPresenter
    {
        private const float SYNC_PERIOD = 0.2f;
        
        public event Action OnStateChanged;
        public event Action<bool> OnBuyButtonEnabled;

        [ShowInInspector]
        public string Title => _currentProduct ? _currentProduct.Title : string.Empty;

        [ShowInInspector]
        public string Description => _currentProduct ? _currentProduct.Description : string.Empty;

        [ShowInInspector]
        public Sprite Icon => _currentProduct ? _currentProduct.Icon : default;

        [ShowInInspector]
        public bool IsBuyButtonEnabled => buyButtonEnabled;

        public IReadOnlyList<IPriceElement> PriceElements => this.priceElements;

        private readonly ProductBuyer _productBuyer;
        private readonly CurrencyBank _currencyBank;
        private Product _currentProduct;

        private readonly List<IPriceElement> priceElements = new();
        private bool buyButtonEnabled;
        private float syncTime;

        public ProductPresenter(ProductBuyer productBuyer, CurrencyBank currencyBank, Product initialProduct = default)
        {
            _productBuyer = productBuyer;
            _currencyBank = currencyBank;

            this.SetProduct(initialProduct);
        }

        void IInitializable.Initialize()
        {
            this.buyButtonEnabled = this.CanBuy();
        }

        void ILateTickable.LateTick()
        {
            this.syncTime += Time.deltaTime;
            if (this.syncTime < SYNC_PERIOD)
            {
                return;
            }
            
            this.syncTime -= SYNC_PERIOD;
            
            bool canBuy = this.CanBuy();
            if (canBuy != this.buyButtonEnabled)
            {
                this.buyButtonEnabled = canBuy;
                this.OnBuyButtonEnabled?.Invoke(this.IsBuyButtonEnabled);
            }
        }

        public void SetProduct(Product product)
        {
            if (_currentProduct != product)
            {
                _currentProduct = product;
                this.UpdatePrice(product);
                this.OnStateChanged?.Invoke();
            }
        }

        private void UpdatePrice(Product product)
        {
            this.priceElements.Clear();

            if (product == null)
            {
                return;
            }

            for (int i = 0; i < product.Price.Count; i++)
            {
                this.priceElements.Add(new PriceElement(this, i));
            }
        }

        public void OnBuyClicked()
        {
            if (_currentProduct)
            {
                _productBuyer.Buy(_currentProduct);
            }
        }

        private bool CanBuy()
        {
            return _currentProduct && _productBuyer.CanBuy(_currentProduct);
        }

        private sealed class PriceElement : IPriceElement
        {
            public string Amount => this.GetPriceElement().amount.ToString();
            public Sprite Icon => _parent._currencyBank.GetCell(this.GetPriceElement().type).Icon;

            private readonly int _index;
            private readonly ProductPresenter _parent;

            public PriceElement(ProductPresenter parent, int index)
            {
                _index = index;
                _parent = parent;
            }

            private CurrencyData GetPriceElement()
            {
                return _parent._currentProduct.Price[_index];
            }
        }
    }
}