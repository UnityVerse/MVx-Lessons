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

        [SerializeField]
        private ProductPopup productPopup;

        public override void InstallBindings()
        {
            this.Container
                .Bind<ProductPopup>()
                .FromInstance(this.productPopup)
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesAndSelfTo<CurrencyBankPresenter>()
                .AsCached()
                .WithArguments(this.currencyListView)
                .NonLazy();

            this.Container.Bind<ProductShower>().AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<ProductPresenter>()
                .AsSingle()
                .WithArguments(this.initialProduct)
                .NonLazy();
        }
    }
}