// using DG.Tweening;
// using TMPro;
// using UnityEngine;
//
// namespace Lessons.Architecture.MVO
// {
//     //Логика представления:
//     public sealed class CurrencyPanel : MonoBehaviour
//     {
//         [SerializeField]
//         private TextMeshProUGUI currencyText;
//
//         public void SetupCurrency(string currency)
//         {
//             this.currencyText.text = currency;
//         }
//         
//         public void UpdateCurrency(string currency)
//         {
//             this.currencyText.text = currency;
//
//             //Animation:
//             DOTween.Sequence() //Логика представления
//                 .Append(this.currencyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
//                 .Append(this.currencyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
//         }
//     }
// }