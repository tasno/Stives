namespace StIvesLib.Meeples
{
    public class Husband : Meeple
    {
        public Husband()
            : base()
        {
            SleepingAtInn = true;
        }
        public override string Name
        {
            get { return "Husband"; }
        }
        public override int FarmingIncome
        {
            get { return 12; }
        }
        public override bool MayDrive
        {
            get
            {
                return true;
            }
        }
        public override bool MayMarry
        {
            get
            {
                return true;
            }
        }
    }
}
