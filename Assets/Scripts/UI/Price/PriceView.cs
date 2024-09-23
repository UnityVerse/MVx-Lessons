using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PriceView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text amount;

        [SerializeField]
        private Image icon;

        public void SetAmount(string amount)
        {
            this.amount.text = amount;
        }

        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}