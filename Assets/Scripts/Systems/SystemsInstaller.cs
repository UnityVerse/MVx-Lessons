using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private int initialMoney = 1000;

        [SerializeField]
        private short initialGems = 50;

        public override void InstallBindings()
        {
            ProductInstaller.Install(this.Container);
            CurrencyInstaller.Install(this.Container, this.initialMoney, this.initialGems);
        }
    }
}