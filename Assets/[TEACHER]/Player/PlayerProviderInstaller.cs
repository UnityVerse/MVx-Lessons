using Zenject;

namespace SampleGame
{
    public sealed class PlayerProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<PlayerProvider>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}