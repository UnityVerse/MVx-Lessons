using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyListView currencyListView;

        [SerializeField]
        private LootboxListView lootboxListView;

        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<CurrencyBankPresenter>()
                .AsCached()
                .WithArguments(this.currencyListView)
                .NonLazy();

            this.Container
                .BindInterfacesTo<LootboxListPresenter>()
                .AsSingle()
                .WithArguments(this.lootboxListView)
                .NonLazy();

            this.Container
                .BindFactory<Lootbox, LootboxView, LootboxPresenter, LootboxPresenter.Factory>()
                .AsSingle()
                .NonLazy();


            // this.Container
            //     .BindInterfacesTo<LootboxPresenter>()
            //     .AsSingle()
            //     .WithArguments(this.lootboxView)
            //     .NonLazy();
        }
    }
}

// [SerializeField]
// private Product initialProduct;
//
// [SerializeField]
// private ProductPopup productPopup;

// this.Container
//     .Bind<ProductPopup>()
//     .FromInstance(this.productPopup)
//     .AsSingle()
//     .NonLazy();
//
// this.Container.Bind<ProductShower>().AsSingle();
//
// this.Container
//     .BindInterfacesAndSelfTo<ProductPresenter>()
//     .AsSingle()
//     .WithArguments(this.initialProduct)
//     .NonLazy();