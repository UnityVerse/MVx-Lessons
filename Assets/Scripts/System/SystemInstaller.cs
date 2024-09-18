using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemInstaller : MonoInstaller
    {
        [SerializeField]
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