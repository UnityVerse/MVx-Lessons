using UnityEngine;

namespace SampleGame
{
    public sealed class CurrencyListView : MonoBehaviour
    {
        [SerializeField]
        private CurrencyView itemPrefab;

        [SerializeField]
        private Transform container;

        public ICurrencyView SpawnItem()
        {
            return Instantiate(this.itemPrefab, this.container);
        }

        public void UnspawnItem(ICurrencyView item)
        {
            if (item != null)
            {
                Destroy(((CurrencyView) item).gameObject);
            }
        }
    }
}