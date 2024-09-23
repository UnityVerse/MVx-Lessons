using Zenject;

namespace SampleGame
{
    public sealed class CurrencyInstaller : Installer<int, short, CurrencyInstaller>
    {
        [Inject]
        private int initialMoney;

        [Inject]
        private short initialGems;
        
        public override void InstallBindings()
        {
            this.Container
                .Bind<MoneyStorage>()
                .AsSingle()
                .OnInstantiated<MoneyStorage>((_, it) => it.SetupMoney(this.initialMoney))
                .NonLazy();

            this.Container
                .Bind<GemsStorage>()
                .AsSingle()
                .OnInstantiated<GemsStorage>((_, storage) => storage.SetupGems(this.initialGems))
                .NonLazy();
        }
    }
}