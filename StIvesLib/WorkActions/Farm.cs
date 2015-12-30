using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class Farm : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Farm; }
        }

        public override void Perform(IMeeple meeple)
        {
            meeple.Player.Earn(meeple.FarmingIncome);
        }
    }
}
