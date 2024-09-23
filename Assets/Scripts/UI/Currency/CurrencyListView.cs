using UnityEngine;

namespace SampleGame
{
    public sealed class CurrencyListView : MonoBehaviour
    {
        [SerializeField]
        private CurrencyView itemPrefab;

        [SerializeField]
        private Transform container;

        public CurrencyView SpawnItem()
        {
            return Instantiate(this.itemPrefab, this.container);
        }

        public void UnspawnItem(CurrencyView item)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }
    }
}