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
        private PriceListView priceListView;

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

            _presenter.OnBuyButtonEnabled += this.OnBuyButtonStateEnabled;
            _presenter.OnStateChanged += this.OnStateChanged;

            this.buyButton.onClick.AddListener(_presenter.OnBuyClicked);
            this.gameObject.SetActive(true);
        }

        [Button]
        public void Hide()
        {
            _presenter.OnBuyButtonEnabled -= this.OnBuyButtonStateEnabled;
            _presenter.OnStateChanged += this.OnStateChanged;

            this.buyButton.onClick.RemoveListener(_presenter.OnBuyClicked);
            this.gameObject.SetActive(false);
        }

        private void OnBuyButtonStateEnabled(bool enabled)
        {
            this.buyButton.interactable = enabled;
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
            this.priceListView.Clear();

            IReadOnlyList<IPriceElement> elements = _presenter.PriceElements;
            for (int i = 0, count = elements.Count; i < count; i++)
            {
                IPriceElement presenter = elements[i];
                PriceView priceView = this.priceListView.SpawnItem();
                priceView.SetAmount(presenter.Amount);
                priceView.SetIcon(presenter.Icon);
            }
        }
    }
}