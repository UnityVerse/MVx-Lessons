using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyCell[] cells;

        public override void InstallBindings()
        {
            ProductInstaller.Install(this.Container);
            CurrencyInstaller.Install(this.Container, this.cells);
        }
    }
}