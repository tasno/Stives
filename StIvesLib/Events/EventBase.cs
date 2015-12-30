using StIves.Interfaces;
using StIves.Interfaces.Actions;

namespace StIvesLib.Events
{
    public abstract class EventBase : IEvent
    {
        public abstract string Name { get; }
        public abstract string Prompt { get; }
        public virtual ActionRequest GetActionRequest(IPlayer player)
        {
            return new ActionRequest(player, null, ActionRequestType.DrawEvent, string.Format("{0}: {1}.", Name, Prompt));
        }
    }
}
