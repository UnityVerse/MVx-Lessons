using System.Collections.Generic;
using UnityEngine;

namespace SampleGame
{
    public abstract class ListView<T> : MonoBehaviour where T : Component
    {
        [SerializeField]
        private T itemPrefab;

        [SerializeField]
        private Transform container;
        
        private readonly List<T> items = new();
        private readonly Queue<T> freeList = new();

        public T SpawnElement()
        {
            if (this.freeList.TryDequeue(out var item))
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Instantiate(this.itemPrefab, this.container);
            }
            
            this.items.Add(item);
            return item;
        }

        public void DespawnElement(T item)
        {
            if (item != null && this.items.Remove(item))
            {
                item.gameObject.SetActive(false);
                this.freeList.Enqueue(item);
            }
        }
        
        public void Clear()
        {
            for (int i = 0, count = this.items.Count; i < count; i++)
            {
                T item = this.items[i];
                item.gameObject.SetActive(false);
                this.freeList.Enqueue(item);
            }
            
            this.items.Clear();
        }
    }
}