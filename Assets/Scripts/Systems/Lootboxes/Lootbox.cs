using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    [Serializable]
    public sealed class Lootbox : IFixedTickable
    {
        public event Action<Lootbox, bool> OnReady;
        public event Action<Lootbox, float> OnTimerTicked;

        public bool IsReady => this.isReady;
        public float RemainingTime => this.remainingTime;
        public float Duration => this.duration;
        public float Progress => 1 - this.remainingTime / this.duration;
        public IReadOnlyList<CurrencyData> CurrencyReward => this.currencyReward;

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
        [SerializeField, PreviewField]
        private Sprite icon;

        public bool Consume()
        {
            if (!this.isReady)
            {
                return false;
            }

            this.isReady = false;
            this.remainingTime = this.duration;
            this.OnReady?.Invoke(this, false);
            return true;
        }

        void IFixedTickable.FixedTick()
        {
            if (this.isReady)
            {
                return;
            }

            this.remainingTime = Mathf.Max(0, this.remainingTime - Time.fixedDeltaTime);
            this.OnTimerTicked?.Invoke(this, this.remainingTime);

            if (this.remainingTime <= 0)
            {
                this.SetReady();
            }
        }

        private void SetReady()
        {
            this.isReady = true;
            this.OnReady?.Invoke(this, true);
        }
    }
}