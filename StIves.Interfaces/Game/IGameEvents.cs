using StIves.Interfaces.Actions;

namespace StIves.Interfaces
{
    public interface IGameEvents
    {
        IEvent NextEvent { get; }
    }
}