using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SampleGame
{
    //Passive View
    public sealed class LootboxView : MonoBehaviour
    {
        public event UnityAction OnCollectClicked
        {
            add { this.collectButton.onClick.AddListener(value); }
            remove { this.collectButton.onClick.RemoveListener(value); }
        }

        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private TMP_Text remainingTimeText;

        [SerializeField]
        private Button collectButton;

        [SerializeField]
        private GameObject timerView;

        [SerializeField]
        private AmountListView resourceElements;

        public void SetIcon(Sprite icon)
        {
            this.iconImage.sprite = icon;
        }

        public void SetRemainingTime(string time)
        {
            this.remainingTimeText.text = time;
        }

        public void SetReady(bool isReady)
        {
            this.collectButton.gameObject.SetActive(isReady);
            this.timerView.SetActive(!isReady);
        }

        public AmountView SpawnResourceElement()
        {
            return this.resourceElements.SpawnElement();
        }

        public void ClearResourceElements()
        {
            this.resourceElements.Clear();
        }
    }
}