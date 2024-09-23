using Zenject;

namespace SampleGame
{
    public sealed class CurrencyInstaller : Installer<CurrencyCell[], CurrencyInstaller>
    {
        [Inject]
        private CurrencyCell[] cells;
        
        public override void InstallBindings()
        {
            this.Container
                .Bind<CurrencyBank>()
                .AsSingle()
                .WithArguments(this.cells)
                .NonLazy();
        }
    }
}