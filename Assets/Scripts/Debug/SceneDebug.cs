using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SceneDebug : MonoBehaviour
    {
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private CurrencyBank currencyBank;

        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductBuyer productBuyer;
        
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductPresenter presenter;
        
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductShower productShower;
        
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private LootboxManager lootboxManager;
    }
}