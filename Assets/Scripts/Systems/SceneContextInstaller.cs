using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SceneContextInstaller : MonoInstaller
    {
        [SerializeField]
        private int initialMoney = 1000;
        
        public override void InstallBindings()
        {
            this.Container
                .Bind<MoneyStorage>()
                .AsSingle()
                .WithArguments(this.initialMoney)
                .NonLazy();
        }
    }
}