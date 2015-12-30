using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;
using StIvesLib.Interfaces;

namespace StIvesLib.Phases
{
    internal class Day : Phase
    {
        internal Day(IGameInternal game)
            : base(game)
        {
        }

        public override IPhase NextPhase
        {
            get { return new Evening(TheGame); }
        }

        public override PhaseType Type
        {
            get { return PhaseType.Day; }
        }

        public override void Action()
        {
            var actionsTaken = new List<IWorkAction>();

            foreach (var meeple in TheGame.Players.SelectMany(p => p.Meeples))
            {
                meeple.ResetBillet();
                if (null != meeple.DayAction
                    && !actionsTaken.Contains(meeple.DayAction))
                {
                    actionsTaken.Add(meeple.DayAction);
                }
            }
            foreach (var action in actionsTaken)
            {
                foreach (var player in TheGame.Players)
                {
                    foreach (var meeple in player.Meeples.Where(m => action == m.DayAction))
                    {
                        action.Perform(meeple);
                    }
                    action.PerformPlayerDayAction(player, this);
                }
            }
        }
    }
}
