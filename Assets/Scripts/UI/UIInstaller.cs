using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyView moneyView;

        [SerializeField]
        private CurrencyView gemsView;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<MoneyPresenter>()
                .AsCached()
                .WithArguments(this.moneyView)
                .NonLazy();

            this.Container
                .BindInterfacesTo<GemsPresenter>()
                .AsCached()
                .WithArguments(this.gemsView)
                .NonLazy();
        }
    }
}