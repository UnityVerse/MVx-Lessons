// namespace _Teacher_
// {
//
// public interface IProductPresenter
// {
//     event Action OnStateChanged;
//     event Action<bool> OnBuyButtonInteractible;
//         
//     string Title { get; }
//     string Description { get; }
//     Sprite Icon { get; }
//     string Price { get; }
//         
//     bool IsBuyButtonInteractible { get; }
//         
//     void OnBuyClick();
// }

//    public sealed class ProductPresenter : IProductPresenter, IInitializable, IDisposable
//     {
//         public event Action OnStateChanged;
//         public event Action<bool> OnBuyButtonInteractible;
//
//         [ShowInInspector]
//         public string Title => _product != null ? _product.title : default;
//
//         [ShowInInspector]
//         public string Description => _product != null ? _product.description : default;
//
//         [ShowInInspector]
//         public Sprite Icon => _product != null ? _product.icon : default;
//
//         [ShowInInspector]
//         public string Price => _product != null ? _product.price.ToString() : default;
//
//         [ShowInInspector]
//         public bool IsBuyButtonInteractible => this.buyButtonInteractible;
//
//         private readonly CurrencyCell _moneyStorage;
//         private readonly ProductBuyer _productBuyer;
//         
//         [ShowInInspector]
//         private Product _product;
//
//         private bool buyButtonInteractible;
//
//         public ProductPresenter(CurrencyBank currencyBank, ProductBuyer productBuyer, Product product = default)
//         {
//             _moneyStorage = currencyBank.GetCell(CurrencyType.MONEY);
//             _productBuyer = productBuyer;
//             _product = product;
//         }
//
//         [Button]
//         public void SetProduct(Product product)
//         {
//             if (product != _product)
//             {
//                 _product = product;
//                 this.buyButtonInteractible = this.CanBuy();
//                 this.OnStateChanged?.Invoke();    
//             }
//         }
//
//         public void Initialize()
//         {
//             this.buyButtonInteractible = this.CanBuy();
//             _moneyStorage.OnStateChanged += this.OnMoneyChanged;
//         }
//
//         public void Dispose()
//         {
//             _moneyStorage.OnStateChanged -= this.OnMoneyChanged;
//         }
//
//         public void OnBuyClick()
//         {
//             _productBuyer.Buy(_product);
//         }
//
//         private void OnMoneyChanged()
//         {
//             bool buyButtonInteractible = this.CanBuy();
//             if (buyButtonInteractible != this.buyButtonInteractible)
//             {
//                 this.buyButtonInteractible = buyButtonInteractible;
//                 this.OnBuyButtonInteractible?.Invoke(buyButtonInteractible);
//             }
//         }
//
//         private bool CanBuy()
//         {
//             return _product != null && _productBuyer.CanBuy(_product);
//         }
//     }
// }