// using System;
// using Sirenix.OdinInspector;
//
// namespace SampleGame
// {
//     public sealed class GemsStorage
//     {
//         public event Action<int> OnGemsChanged;
//
//         [field: ReadOnly]
//         [field: ShowInInspector]
//         public int Gems { get; private set; }
//
//         public GemsStorage(int gems) => this.Gems = gems;
//
//         [Button]
//         public void SetupGems(int gems) => this.Gems = gems;
//
//         [Button]
//         public void AddGems(int range)
//         {
//             this.Gems += range;
//             this.OnGemsChanged?.Invoke(this.Gems);
//         }
//
//         [Button]
//         public void SpendGems(int range)
//         {
//             this.Gems -= range;
//             this.OnGemsChanged?.Invoke(this.Gems);
//         }
//     }
// }