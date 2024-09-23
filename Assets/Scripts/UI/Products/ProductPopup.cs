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
        
        private IProductPresenter _presenter;
        
        [Inject]
        public void Construct(IProductPresenter presenter)
        {
            _presenter = presenter;
        }

        [Sirenix.OdinInspector.Button]
        public void Show()
        {
            this.title.text = _presenter.Title;
            this.description.text = _presenter.Description;
            this.icon.sprite = _presenter.Icon;
            this.price.text = _presenter.Price;
            
            this.buyButton.interactable = _presenter.IsBuyButtonInteractible;
            this.buyButton.onClick.AddListener(_presenter.OnBuyClick);

            _presenter.OnBuyButtonInteractible += this.OnBuyButtonInteractible;

            this.gameObject.SetActive(true);
        }

        [Sirenix.OdinInspector.Button]
        public void Hide()
        {
            _presenter.OnBuyButtonInteractible -= this.OnBuyButtonInteractible;

            this.buyButton.onClick.RemoveListener(_presenter.OnBuyClick);
            this.gameObject.SetActive(false);
        }

        private void OnBuyButtonInteractible(bool interactible)
        {
            this.buyButton.interactable = interactible;
        }
    }
}