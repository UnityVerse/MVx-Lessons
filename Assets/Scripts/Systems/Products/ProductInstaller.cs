using Zenject;

namespace SampleGame
{
    public sealed class ProductInstaller : Installer<ProductInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<ProductBuyer>().AsSingle();
        }
    }
}