using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class ProductPopup : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text title;

        [SerializeField]
        private TextMeshProUGUI description;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text price;
        
        [SerializeField]
        private Button buyButton;

        [Inject]
        private ProductBuyer productBuyer;

        [Inject]
        private MoneyStorage moneyStorage;

        [SerializeField]
        private Product product;

        public void SetProduct(Product product)
        {
            this.product = product;
        }

        [Sirenix.OdinInspector.Button]
        public void Show()
        {
            this.title.text = product.title;
            this.description.text = product.description;
            this.icon.sprite = product.icon;
            this.price.text = product.price.ToString();
            
            this.buyButton.interactable = this.productBuyer.CanBuy(product);
            this.buyButton.onClick.AddListener(this.OnBuyClicked);

            this.moneyStorage.OnStateChanged += this.OnMoneyChanged;
            this.gameObject.SetActive(true);
        }

        [Sirenix.OdinInspector.Button]
        public void Hide()
        {
            this.buyButton.onClick.RemoveListener(this.OnBuyClicked);
            this.moneyStorage.OnStateChanged -= this.OnMoneyChanged;
            this.gameObject.SetActive(false);
        }
        
        private void OnBuyClicked()
        {
            this.productBuyer.Buy(product);
        }

        private void OnMoneyChanged(int _)
        {
            this.buyButton.interactable = this.productBuyer.CanBuy(product);
        }
    }
}