using System.Text;
using StIves.Interfaces;
using StIvesLib.Interfaces;

namespace StIvesLib.Phases
{
    internal class Night : Phase
    {
        internal Night(IGameInternal game)
            : base(game)
        {
        }

        public override IPhase NextPhase
        {
            get { return new Morning(TheGame); }
        }

        public override PhaseType Type
        {
            get { return PhaseType.Night; }
        }

        public override void Action()
        {
            foreach (var location in TheGame.Locations.LocationsWithPlayers())
            {
                TheGame.Locations.SetInnOption(location);
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine(base.ToString());
            result.Append(TheGame.Locations.InnSummary);
            return result.ToString();
        }
    }
}
