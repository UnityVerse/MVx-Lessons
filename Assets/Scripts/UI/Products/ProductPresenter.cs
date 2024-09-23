using System;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class ProductPresenter : IProductPresenter, IInitializable, IDisposable
    {
        public event Action<bool> OnBuyButtonInteractible;

        public string Title => _product != null ? _product.title : default;
        public string Description => _product != null ? _product.description : default;
        public Sprite Icon => _product != null ? _product.icon : default;
        public string Price => _product != null ? _product.price.ToString() : default;
        
        public bool IsBuyButtonInteractible => this.buyButtonInteractible;
        
        private readonly CurrencyCell _currencyBank;
        private readonly ProductBuyer _productBuyer;
        private Product _product;

        private bool buyButtonInteractible;
        
        public ProductPresenter(CurrencyBank currencyBank, ProductBuyer productBuyer, Product product = default)
        {
            _currencyBank = currencyBank.GetCell(CurrencyType.MONEY);
            _productBuyer = productBuyer;
            _product = product;
        }
        
        public void Initialize()
        {
            this.buyButtonInteractible = this.CanBuy();
            _currencyBank.OnStateChanged += this.OnMoneyChanged;
        }

        public void Dispose()
        {
            _currencyBank.OnStateChanged -= this.OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            bool buyButtonInteractible = this.CanBuy();
            if (buyButtonInteractible != this.buyButtonInteractible)
            {
                this.buyButtonInteractible = buyButtonInteractible;
                this.OnBuyButtonInteractible?.Invoke(buyButtonInteractible);
            }
        }

        private bool CanBuy()
        {
            return _product != null && _productBuyer.CanBuy(_product);
        }

        public void OnBuyClick()
        {
            _productBuyer.Buy(_product);
        }
        
        //
        //     this.

        //
        //
        //
        // private void OnMoneyChanged(int _)
        // {
        //     this.buyButton.interactable = this.productBuyer.CanBuy(product);
        // }
        //
        // this.title.text = _presenter.Title;
        // this.description.text = product.description;
        // this.icon.sprite = product.icon;
        // this.price.text = product.price.ToString();
        //     
        // this.buyButton.interactable = this.productBuyer.CanBuy(product);
        // this.buyButton.onClick.AddListener(this.OnBuyClicked);
        //
        //
        // private void OnBuyClicked()
        // {
        //     this.productBuyer.Buy(product);
        // }


      
    }
}