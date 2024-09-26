using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class Lootbox
    {
        public event Action<Lootbox> OnConsumed;
        public event Action<Lootbox> OnReady;
        public event Action<Lootbox> OnTimerTicked;

        public bool IsReady => this.isReady;
        public float RemainingTime => this.remainingTime;
        public float Duration => this.duration;
        public float Progress => 1 - this.remainingTime / this.duration;
        public IReadOnlyList<CurrencyData> CurrencyReward => this.currencyReward;

        public string Title => this.title;
        public Sprite Icon => this.icon;

        [SerializeField]
        private float remainingTime;

        [SerializeField]
        private float duration;
        
        [SerializeField]
        private bool isReady;

        [SerializeField]
        private CurrencyData[] currencyReward;

        [Header("Meta")]
        [SerializeField]
        private string title;

        [SerializeField, PreviewField]
        private Sprite icon;

        public void TickTimer(float deltaTime)
        {
            if (this.isReady)
            {
                return;
            }

            this.remainingTime = Mathf.Max(0, this.remainingTime - deltaTime);
            this.OnTimerTicked?.Invoke(this);

            if (this.remainingTime <= 0)
            {
                this.SetReady();
            }
        }

        private void SetReady()
        {
            this.isReady = true;
            this.OnReady?.Invoke(this);
        }

        public bool Consume()
        {
            if (!this.isReady)
            {
                return false;
            }

            this.isReady = false;
            this.remainingTime = this.duration;
            this.OnConsumed?.Invoke(this);
            return true;
        }
    }
}