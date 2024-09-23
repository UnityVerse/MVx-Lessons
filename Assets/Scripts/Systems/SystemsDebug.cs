using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemsDebug : MonoBehaviour
    {
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private MoneyStorage moneyStorage;
        
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private GemsStorage gemsStorage;

        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductBuyer productBuyer;
    }
}