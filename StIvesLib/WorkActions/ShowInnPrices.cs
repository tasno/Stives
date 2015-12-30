using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class ShowInnPrices : WorkAction
    {
        public override WorkActionType Type
        {
            get
            {
                return WorkActionType.ListInns;
            }
        }

        public override void Perform(IMeeple meeple)
        {
        }
    }
}
