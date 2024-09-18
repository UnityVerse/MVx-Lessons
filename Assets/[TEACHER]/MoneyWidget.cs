// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using TMPro;
// using UnityEngine;
// using Zenject;
//
// // ReSharper disable FieldCanBeMadeReadOnly.Local
//
// namespace SampleGame
// {
//     public sealed class MoneyWidget : MonoBehaviour
//     {
//         [SerializeField]
//         private TMP_Text moneyText;
//
//         [SerializeField]
//         private float animationDuration = 1.0f;
//
//         [SerializeField]
//         private Color spendColor;
//
//         [SerializeField]
//         private Color earnColor;
//
//         private Coroutine _animationCoroutine;
//         private List<Sequence> _animationSequences = new();
//
//         private MoneyStorage moneyStorage;
//
//         [Inject]
//         public void Construct(MoneyStorage moneyStorage)
//         {
//             this.moneyStorage = moneyStorage;
//         }
//
//         private void OnEnable()
//         {
//             this.moneyText.text = this.moneyStorage.Money.ToString();
//
//             this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
//             this.moneyStorage.OnMoneyEarned += this.OnMoneyAdded;
//             this.moneyStorage.OnMoneySpent += this.OnMoneyRemoved;
//         }
//
//         private void OnDisable()
//         {
//             this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
//             this.moneyStorage.OnMoneyEarned -= this.OnMoneyAdded;
//             this.moneyStorage.OnMoneySpent -= this.OnMoneyRemoved;
//         }
//
//         private void OnMoneyChanged(int newvalue, int previousvalue)
//         {
//             this.StopAnimations();
//             this.moneyText.text = this.moneyStorage.Money.ToString();
//             this.BounceAnimation();
//         }
//
//         private void OnMoneyRemoved(int newvalue, int range)
//         {
//             this.StopAnimations();
//
//             this.moneyText.text = this.moneyStorage.Money.ToString();
//
//             this.BounceAnimation();
//             this.ColorAnimation(this.spendColor);
//         }
//
//         private void OnMoneyAdded(int newvalue, int range)
//         {
//             this.StopAnimations();
//
//             _animationCoroutine = this.StartCoroutine(this.AddMoneyAnimation(newvalue - range, range));
//             this.BounceAnimation();
//             this.ColorAnimation(this.earnColor, this.animationDuration - 0.3f);
//         }
//
//         private void ColorAnimation(Color color, float interval = 0.5f)
//         {
//             Sequence sequence = DOTween.Sequence();
//             sequence
//                 .AppendCallback(() => _animationSequences.Add(sequence))
//                 .Append(this.moneyText.DOColor(color, 0.1f))
//                 .AppendInterval(interval)
//                 .Append(this.moneyText.DOColor(Color.black, 0.3f))
//                 .OnComplete(() => _animationSequences.Remove(sequence));
//         }
//
//         private void BounceAnimation()
//         {
//             Sequence sequence = DOTween.Sequence();
//             sequence
//                 .AppendCallback(() => _animationSequences.Add(sequence))
//                 .Append(this.moneyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.2f))
//                 .Append(this.moneyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f))
//                 .OnComplete(() => _animationSequences.Remove(sequence));
//         }
//
//         private void StopAnimations()
//         {
//             if (_animationCoroutine != null)
//             {
//                 this.StopCoroutine(_animationCoroutine);
//                 _animationCoroutine = null;
//             }
//
//             foreach (var sequence in _animationSequences)
//             {
//                 sequence.Kill();
//             }
//
//             _animationSequences.Clear();
//         }
//
//         private IEnumerator AddMoneyAnimation(int startMoney, int range)
//         {
//             float progress = 0;
//
//             while (progress <= 1)
//             {
//                 yield return null;
//                 progress = Mathf.Min(1, progress + Time.deltaTime / this.animationDuration);
//
//                 int currentMoney = Mathf.RoundToInt(startMoney + range * progress);
//                 this.moneyText.text = currentMoney.ToString();
//             }
//
//             this.moneyText.text = (startMoney + range).ToString();
//         }
//     }
// }