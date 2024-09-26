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
                .BindFactory<Lootbox, LootboxView, LootboxPresenter, LootboxPresenter.Factory>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesTo<LootboxListPresenter>()
                .AsCached()
                .WithArguments(this.lootboxListView)
                .NonLazy();
        }
    }
}