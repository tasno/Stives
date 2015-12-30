using StIves.Interfaces;
using StIvesLib.Interfaces;

namespace StIvesLib
{
    public abstract class Meeple : IMeepleInternal
    {
        public abstract string Name { get; }
        public virtual bool CanWork { get { return SleepingAtInn.HasValue && SleepingAtInn.Value; } }
        public bool BilletAssigned { get { return SleepingAtInn.HasValue; } }
        public void ResetBillet()
        {
            SleepingAtInn = null;
        }
        public virtual int DefaultBillet { get { return 0; } }

        public void Billet(bool atInn)
        {
            SleepingAtInn = atInn;
        }

        protected bool? SleepingAtInn { get; set; }

        public abstract int FarmingIncome { get; }

        public virtual int MaxSacks { get { return 1; } }
        public virtual int MaxCats { get { return 1; } }
        public virtual int MaxKittens { get { return 1; } }
        public virtual bool MayDrive { get { return false; } }
        public virtual bool MayMarry { get { return false; } }
        public virtual bool ShootsFirst { get { return false; } }

        public IPlayer Player { get; set; }
        public IWorkAction DayAction { get; set; }
        public bool Travelling { get { return null == DayAction || WorkActionType.Travel == DayAction.Type; } }
        public override string ToString()
        {
            return Name;
        }
    }
}
