using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class CurrencyView : MonoBehaviour, ICurrencyView
    {
        [SerializeField]
        private TMP_Text currencyText;

        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private float animationDuration = 1.0f;

        [SerializeField]
        private Color spendColor;

        [SerializeField]
        private Color earnColor;

        private Coroutine _animationCoroutine;
        private readonly List<Sequence> _animationSequences = new();

        public void SetIcon(Sprite icon)
        {
            this.iconImage.sprite = icon;
        }
        
        public void SetupCurrency(string currency)
        {
            this.currencyText.text = currency;
        }

        public void ChangeCurrency(string currency)
        {
            this.StopAnimations();
            this.currencyText.text = currency;
            this.BounceAnimation();
        }

        public void RemoveCurrency(string currency)
        {
            this.StopAnimations();

            this.currencyText.text = currency;

            this.BounceAnimation();
            this.ColorAnimation(this.spendColor);
        }

        public void AddCurrency(int newvalue, int range)
        {
            this.StopAnimations();

            _animationCoroutine = this.StartCoroutine(this.AddMoneyAnimation(newvalue - range, range));
            this.BounceAnimation();
            this.ColorAnimation(this.earnColor, this.animationDuration - 0.3f);
        }

        private void ColorAnimation(Color color, float interval = 0.5f)
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendCallback(() => _animationSequences.Add(sequence))
                .Append(this.currencyText.DOColor(color, 0.1f))
                .AppendInterval(interval)
                .Append(this.currencyText.DOColor(Color.black, 0.3f))
                .OnComplete(() => _animationSequences.Remove(sequence));
        }

        private void BounceAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendCallback(() => _animationSequences.Add(sequence))
                .Append(this.currencyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.2f))
                .Append(this.currencyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f))
                .OnComplete(() => _animationSequences.Remove(sequence));
        }

        private void StopAnimations()
        {
            if (_animationCoroutine != null)
            {
                this.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }

            foreach (var sequence in _animationSequences)
            {
                sequence.Kill();
            }

            _animationSequences.Clear();
        }

        private IEnumerator AddMoneyAnimation(int startMoney, int range)
        {
            float progress = 0;

            while (progress <= 1)
            {
                yield return null;
                progress = Mathf.Min(1, progress + Time.deltaTime / this.animationDuration);

                int currentMoney = Mathf.RoundToInt(startMoney + range * progress);
                this.currencyText.text = currentMoney.ToString();
            }

            this.currencyText.text = (startMoney + range).ToString();
        }
    }
}