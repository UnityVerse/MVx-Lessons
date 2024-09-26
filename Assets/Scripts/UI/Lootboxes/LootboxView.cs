using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class LootboxView : MonoBehaviour
    {
        public event UnityAction OnCollectClicked
        {
            add { this.collectButton.onClick.AddListener(value); }
            remove { this.collectButton.onClick.RemoveListener(value); }
        }

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text remainingTime;

        [SerializeField]
        private GameObject timerView;

        [SerializeField]
        private Button collectButton;

        [SerializeField]
        private PriceListView priceElements;
        
        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }

        public void SetReady(bool ready)
        {
            this.timerView.SetActive(!ready);
            this.collectButton.gameObject.SetActive(ready);
        }

        public void SetRemainingTime(string time)
        {
            this.remainingTime.text = time;
        }

        public void ClearPriceElements()
        {
            this.priceElements.Clear();
        }

        public PriceView SpawnPriceElement()
        {
            return this.priceElements.SpawnElement();
        }
    }
}