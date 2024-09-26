using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static SampleGame.IProductPresenter;

namespace SampleGame
{
    public sealed class ProductPopup : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text title;

        [SerializeField]
        private TMP_Text description;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private Button buyButton;

        [SerializeField]
        private AmountListView amountListView;
        
        private IProductPresenter _presenter;

        [Inject]
        public void Construct(IProductPresenter presenter)
        {
            _presenter = presenter;
        }

        [Button]
        public void Show()
        {
            this.OnStateChanged();
            
            _presenter.OnStateChanged += this.OnStateChanged;
            _presenter.OnBuyButtonEnabled += this.OnBuyButtonEnabled;

            this.buyButton.onClick.AddListener(_presenter.OnBuyClicked);
            this.gameObject.SetActive(true);
        }
        
        [Button]
        public void Hide()
        {
            _presenter.OnStateChanged -= this.OnStateChanged;
            _presenter.OnBuyButtonEnabled -= this.OnBuyButtonEnabled;

            this.buyButton.onClick.RemoveListener(_presenter.OnBuyClicked);
            this.gameObject.SetActive(false);
        }

        private void OnStateChanged()
        {
            this.title.text = _presenter.Title;
            this.description.text = _presenter.Description;
            this.icon.sprite = _presenter.Icon;
            this.buyButton.interactable = _presenter.IsBuyButtonEnabled;
            this.UpdatePrice();
        }

        private void UpdatePrice()
        {
            this.amountListView.Clear();
            
            IReadOnlyList<IPriceElement> priceElements = _presenter.PriceElements;
            foreach (var presenter in priceElements)
            {
                string price = presenter.Price;
                Sprite icon = presenter.Icon;

                AmountView view = this.amountListView.SpawnElement();
                view.SetAmount(price);
                view.SetIcon(icon);
            }
        }

        private void OnBuyButtonEnabled(bool enabled)
        {
            this.buyButton.interactable = enabled;
        }
    }
}