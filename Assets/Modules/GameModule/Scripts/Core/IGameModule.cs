namespace Modules.GameModule.Scripts.Core
{
    public interface IGameModule
    {
        void Initialize();
        void Enable();
        void Disable();
        void Cleanup();
    }
}