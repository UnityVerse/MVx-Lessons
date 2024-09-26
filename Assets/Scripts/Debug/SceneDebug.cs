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
        private Lootbox lootbox;

        
        [Inject]
        private LootboxConsumer lootboxConsumer;

        [Button]
        private void ConsumeLootbox()
        {
            this.lootboxConsumer.Consume(this.lootbox);
        }
    }
}