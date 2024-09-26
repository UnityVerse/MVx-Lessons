using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyCell[] cells;

        [SerializeField]
        private Lootbox[] lootboxes;

        public override void InstallBindings()
        {
            this.Container
                .Bind<CurrencyBank>()
                .AsSingle()
                .WithArguments(this.cells)
                .NonLazy();
            
            this.Container.Bind<ProductBuyer>().AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<LootboxManager>()
                .AsSingle()
                .WithArguments(this.lootboxes)
                .NonLazy();
        }
    }
}