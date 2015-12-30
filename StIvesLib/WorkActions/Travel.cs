using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using Utils;

namespace StIvesLib.WorkActions
{
    class Travel : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Travel; }
        }

        public override bool CanPerform(IMeeple meeple)
        {
            return meeple.Player.Meeples.Any(m => m.CanWork && m.MayDrive);
        }

        public override void Perform(IMeeple meeple)
        {
            meeple.Player.Location.Move();
        }

        public override void PerformPlayerDayAction(IPlayer player, IAction action)
        {
            if (!player.Meeples.Any(m => m.Travelling && m.MayDrive)) // someone has to drive the cart
            {
                return;
            }
            foreach (var nonTraveller in player.Meeples.Where(m => !m.Travelling))
            {
                nonTraveller.Billet(false);
            }
            for (; ; )
            {
                var gameEvent = _events.Pop();
                if (null == gameEvent)
                {
                    break;
                }
                action.Send(gameEvent.GetActionRequest(player));
            }
        }

        public void AddEvent(IEvent gameEvent)
        {
            _events.Add(gameEvent);
        }
        private readonly IList<IEvent> _events = new List<IEvent>();
    }
}
