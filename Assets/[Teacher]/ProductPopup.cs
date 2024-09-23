// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using Zenject;
// using static SampleGame.IProductPresenter;
//
// namespace SampleGame
// {
//     public sealed class ProductPopup : MonoBehaviour
//     {
//         [SerializeField]
//         private TMP_Text title;
//
//         [SerializeField]
//         private TextMeshProUGUI description;
//
//         [SerializeField]
//         private Image icon;
//
//         [SerializeField]
//         private Button buyButton;
//
//         [SerializeField]
//         private PriceListView priceListView;
//         
//         private IProductPresenter _presenter;
//         
//         [Inject]
//         public void Construct(IProductPresenter presenter)
//         {
//             _presenter = presenter;
//         }
//
//         [Sirenix.OdinInspector.Button]
//         public void Show()
//         {
//             this.OnStateChanged();
//
//             _presenter.OnStateChanged += this.OnStateChanged;
//             _presenter.OnBuyButtonInteractible += this.OnBuyButtonInteractible;
//
//             this.buyButton.onClick.AddListener(_presenter.OnBuyClick);
//             this.gameObject.SetActive(true);
//         }
//
//         [Sirenix.OdinInspector.Button]
//         public void Hide()
//         {
//             _presenter.OnBuyButtonInteractible -= this.OnBuyButtonInteractible;
//             _presenter.OnStateChanged -= this.OnStateChanged;
//
//             this.buyButton.onClick.RemoveListener(_presenter.OnBuyClick);
//             this.gameObject.SetActive(false);
//         }
//         
//         private void OnStateChanged()
//         {
//             this.title.text = _presenter.Title;
//             this.description.text = _presenter.Description;
//             this.icon.sprite = _presenter.Icon;
//             this.buyButton.interactable = _presenter.IsBuyButtonInteractible;
//             this.UpdatePrice();
//         }
//
//         private void UpdatePrice()
//         {
//             this.priceListView.Clear();
//             
//             IReadOnlyList<IPriceElement> presenters = _presenter.PriceElements;
//             for (int i = 0, count = presenters.Count; i < count; i++)
//             {
//                 IPriceElement presenter = presenters[i];
//                 PriceView view = this.priceListView.SpawnItem();
//                 view.SetAmount(presenter.Price);
//                 view.SetIcon(presenter.Icon);
//             }
//         }
//
//         private void OnBuyButtonInteractible(bool interactible)
//         {
//             this.buyButton.interactable = interactible;
//         }
//     }
// }