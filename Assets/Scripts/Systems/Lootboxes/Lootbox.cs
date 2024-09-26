using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class Lootbox
    {
        public event Action<bool> OnReady;
        public event Action<float> OnTimeChanged;

        public bool IsReady => this.isReady;
        public float RemainingTime => this.remainingTime;
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
            this.OnReady?.Invoke(false);
            return true;
        }


        public void Tick(float deltaTime)
        {
            if (this.isReady)
            {
                return;
            }

            this.remainingTime = Mathf.Max(0, this.remainingTime - deltaTime);
            this.OnTimeChanged?.Invoke(this.remainingTime);

            if (this.remainingTime <= 0)
            {
                this.SetReady();
            }
        }

        private void SetReady()
        {
            this.isReady = true;
            this.OnReady?.Invoke(true);
        }
    }
}