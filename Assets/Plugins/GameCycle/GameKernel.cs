using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class GameKernel : MonoKernel,
        IGameStartListener, 
        IGamePauseListener, 
        IGameResumeListener,
        IGameFinishListener
    {
        [Inject]
        private GameManager gameManager;
        
        [InjectLocal]
        private List<IGameListener> listeners = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameTickable> tickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameFixedTickable> fixedTickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameLateTickable> lateTickables = new();

        public override void Start()
        {
            base.Start();
            this.gameManager.AddListener(this);
        }
        
        public override void Update()
        {
            base.Update();

            if (this.gameManager.State == GameState.PLAY)
            {
                float deltaTime = Time.deltaTime;
                foreach (var tickable in this.tickables)
                {
                    tickable.Tick(deltaTime);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.gameManager.State == GameState.PLAY)
            {
                float deltaTime = Time.fixedDeltaTime;
                foreach (var tickable in this.fixedTickables)
                {
                    tickable.FixedTick(deltaTime);
                }
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (this.gameManager.State == GameState.PLAY)
            {
                float deltaTime = Time.deltaTime;
                foreach (var tickable in this.lateTickables)
                {
                    tickable.LateTick(deltaTime);
                }
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            this.gameManager.RemoveListener(this);
        }

        void IGameStartListener.OnStartGame()
        {
            foreach (var it in this.listeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnStartGame();
                }
            }
        }

        void IGamePauseListener.OnPauseGame()
        {
            foreach (var it in this.listeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnPauseGame();
                }
            }
        }

        void IGameResumeListener.OnResumeGame()
        {
            foreach (var it in this.listeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnResumeGame();
                }
            }
        }

        void IGameFinishListener.OnFinishGame()
        {
            foreach (var it in this.listeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnFinishGame();
                }
            }
        }
    }
}