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
        private ProductPresenter productPresenter;
    }
}