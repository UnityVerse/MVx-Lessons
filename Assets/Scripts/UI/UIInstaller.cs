using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyListView currencyListView;

        [SerializeField]
        private Product initialProduct;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<CurrencyBankPresenter>()
                .AsCached()
                .WithArguments(this.currencyListView)
                .NonLazy();
            
            this.Container
                .BindInterfacesAndSelfTo<ProductPresenter>()
                .AsSingle()
                .WithArguments(this.initialProduct)
                .NonLazy();
        }
    }
}