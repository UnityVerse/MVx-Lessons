using System;

namespace SampleGame
{
    public interface IPlayerProvider
    {
        event Action<PlayerType> OnStateChanged;

        PlayerType Current { get; }
        
        T GetService<T>();
    }
}