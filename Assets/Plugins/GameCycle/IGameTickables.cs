namespace SampleGame
{
    public interface IGameTickable
    {
        void Tick(float deltaTime);
    }

    public interface IGamePauseTickable
    {
        void PauseTick(float deltaTime);
    }

    public interface IGameFixedTickable
    {
        void FixedTick(float deltaTime);
    }
    
    public interface IGameLateTickable
    {
        void LateTick(float deltaTime);
    }
}