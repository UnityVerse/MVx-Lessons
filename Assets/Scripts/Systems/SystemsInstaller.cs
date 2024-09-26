using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyCell[] cells;

        [SerializeField]
        private Lootbox[] lootbox;

        public override void InstallBindings()
        {
            this.Container
                .Bind<CurrencyBank>()
                .AsSingle()
                .WithArguments(this.cells)
                .NonLazy();

            this.Container
                .BindInterfacesAndSelfTo<LootboxService>()
                .AsSingle()
                .WithArguments(this.lootbox)
                .NonLazy();

            this.Container
                .Bind<LootboxConsumer>()
                .AsSingle()
                .NonLazy();
        }
    }
}