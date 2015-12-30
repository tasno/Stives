using StIves.Interfaces.Actions;

namespace StIves.Interfaces
{
    public interface IWorkAction
    {
        string Name { get; }
        WorkActionType Type { get; }
        bool CanPerform(IMeeple meeple);
        void Perform(IMeeple meeple);
        void PerformPlayerDayAction(IPlayer player, IAction action);
    }
}
