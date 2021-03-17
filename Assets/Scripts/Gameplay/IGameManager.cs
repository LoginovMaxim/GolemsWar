namespace Gameplay
{
    public interface IGameManager
    {
        ManagerStatus Status { get; }

        void Startup();
    }
}