using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
// ReSharper disable ReturnTypeCanBeEnumerable.Global

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "Product",
        menuName = "Lessons/New Product (Presentation Model)"
    )]
    public sealed class Product : ScriptableObject
    {
        public Sprite Icon => this.icon;
        public string Title => this.title;
        public string Description => this.description;
        public IReadOnlyList<CurrencyData> Price => this.price;
        
        [PreviewField]
        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string title;

        [TextArea]
        [SerializeField]
        private string description;
        
        [SerializeField]
        private CurrencyData[] price;
    }
}