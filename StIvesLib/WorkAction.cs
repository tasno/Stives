using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.WorkActions;

namespace StIvesLib
{
    public abstract class WorkAction : IWorkAction
    {
        static WorkAction()
        {
            AllWorkActions = new WorkAction[] { new Church(), new Travel(), new Sew(), new Catch(), new Feed(), new Farm(), new Cromwell(), new ShowInnPrices(), };
        }
        public virtual string Name { get { return Type.ToString(); } }
        public abstract WorkActionType Type { get; }
        public virtual bool CanPerform(IMeeple meeple)
        {
            return true;
        }
        public abstract void Perform(IMeeple meeple);
        public readonly static WorkAction[] AllWorkActions;

        public virtual void PerformPlayerDayAction(IPlayer player, IAction action)
        {
        }
    }
}
