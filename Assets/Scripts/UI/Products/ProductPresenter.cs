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
        public event Action<bool> OnBuyButtonInteractible;

        [ShowInInspector]
        public string Title => _product != null ? _product.title : default;

        [ShowInInspector]
        public string Description => _product != null ? _product.description : default;

        [ShowInInspector]
        public Sprite Icon => _product != null ? _product.icon : default;

        public IReadOnlyList<IPriceElement> PriceElements => this.priceElements;

        [ShowInInspector]
        public bool IsBuyButtonInteractible => this.buyButtonInteractible;

        private readonly CurrencyBank _currencyBank;
        private readonly ProductBuyer _productBuyer;

        [ShowInInspector]
        private Product _product;

        private readonly List<IPriceElement> priceElements = new();
        private bool buyButtonInteractible;
        private float syncTime;

        public ProductPresenter(CurrencyBank currencyBank, ProductBuyer productBuyer, Product product = default)
        {
            _currencyBank = currencyBank;
            _productBuyer = productBuyer;
            this.SetProduct(product);
        }

        [Button]
        public void SetProduct(Product product)
        {
            if (product == _product)
            {
                return;
            }

            _product = product;

            this.buyButtonInteractible = this.CanBuy();
            this.UpdatePrice(product);
            this.OnStateChanged?.Invoke();
        }

        private void UpdatePrice(Product product)
        {
            this.priceElements.Clear();

            if (product == null)
            {
                return;
            }

            CurrencyData[] price = product.price;
            if (price == null)
            {
                return;
            }
            
            for (int i = 0, count = price.Length; i < count; i++)
            {
                this.priceElements.Add(new PriceElement(this, i));
            }
        }

        public void Initialize()
        {
            this.buyButtonInteractible = this.CanBuy();
        }

        public void OnBuyClick()
        {
            _productBuyer.Buy(_product);
        }

        private bool CanBuy()
        {
            return _product != null && _productBuyer.CanBuy(_product);
        }

        public void LateTick()
        {
            this.syncTime += Time.deltaTime;

            if (this.syncTime > SYNC_PERIOD)
            {
                this.SynchronizeBuyButton();
                this.syncTime -= SYNC_PERIOD;
            }
        }

        private void SynchronizeBuyButton()
        {
            bool buyButtonInteractible = this.CanBuy();
            if (buyButtonInteractible != this.buyButtonInteractible)
            {
                this.buyButtonInteractible = buyButtonInteractible;
                this.OnBuyButtonInteractible?.Invoke(buyButtonInteractible);
            }
        }

        private sealed class PriceElement : IPriceElement
        {
            public string Price => currency.amount.ToString();
            public Sprite Icon => _presenter._currencyBank.GetCell(currency.type).Icon;

            private CurrencyData currency => _presenter._product.price[_priceIndex];

            private readonly ProductPresenter _presenter;
            private readonly int _priceIndex;

            public PriceElement(ProductPresenter presenter, int priceIndex)
            {
                _presenter = presenter;
                _priceIndex = priceIndex;
            }
        }
    }
}