using Sirenix.OdinInspector;

namespace SampleGame
{
    public sealed class ProductShower
    {
        private readonly ProductPopup _productPopup;
        private readonly IProductPresenter _productPresenter;

        public ProductShower(ProductPopup productPopup, IProductPresenter productPresenter)
        {
            _productPopup = productPopup;
            _productPresenter = productPresenter;
        }

        [Button]
        public void Show(Product product)
        {
            _productPresenter.SetProduct(product);
            _productPopup.Show();
        }
    }
}