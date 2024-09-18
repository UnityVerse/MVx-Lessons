using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    internal sealed class PlayerProvider : MonoBehaviour, IPlayerProvider
    {
        public event Action<PlayerType> OnStateChanged;

        [SerializeField]
        private PlayerType currentPlayer = PlayerType.BLUE;

        [ShowInInspector, ReadOnly]
        private readonly Dictionary<PlayerType, DiContainer> players = new();

        public PlayerType Current => this.currentPlayer;

        public T GetService<T>()
        {
            return this.players[this.currentPlayer].Resolve<T>();
        }

        [Button]
        public void ChangePlayer(PlayerType player)
        {
            this.currentPlayer = player;
            this.OnStateChanged?.Invoke(player);
        }

        public void Initialize()
        {
            GameObjectContext[] players = this.GetComponentsInChildren<GameObjectContext>();
            for (int i = 0, count = players.Length; i < count; i++)
            {
                DiContainer playerContainer = players[i].Container;
                PlayerType playerType = playerContainer.Resolve<PlayerType>();
                this.players[playerType] = playerContainer;
            }
        }
    }
}