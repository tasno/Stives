using StIves.Interfaces;
using StIvesLib.Actions;
using StIvesLib.Interfaces;

namespace StIvesLib
{
    internal abstract class Phase : ActionBase, IPhase
    {
        protected Phase(IGameInternal game)
        {
            TheGame = game;
        }

        public abstract IPhase NextPhase { get; }
        public virtual string Description { get { return Type.ToString(); } }
        public abstract PhaseType Type { get; }
        public virtual bool PerPlayer { get { return false; } }

        public virtual void Action(IPlayer player)
        {
        }

        public virtual void Action()
        {
        }

        internal protected IGameInternal TheGame { get; set; }

        public override string ToString()
        {
            return string.Format("{0} of Day {1}.", Description, TheGame.TurnNumber);
        }
    }
}
