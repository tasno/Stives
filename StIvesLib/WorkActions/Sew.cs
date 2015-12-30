using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class Sew : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Sew; }
        }

        public override void Perform(IMeeple meeple)
        {
            meeple.Player.SewSacks(meeple.MaxSacks);
        }
    }
}
