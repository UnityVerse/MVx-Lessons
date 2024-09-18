// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class CurrencyListView : MonoBehaviour
//     {
//         [SerializeField]
//         private CurrencyView prefab;
//
//         [SerializeField]
//         private Transform container;
//         
//         public CurrencyView SpawnView()
//         {
//             return Instantiate(this.prefab, this.container);
//         }
//
//         public void UnspawnView(CurrencyView view)
//         {
//             if (view != null)
//             {
//                 Destroy(view.gameObject);
//             }
//         }
//     }
// }