using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class Church : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Church; }
        }
        public override bool CanPerform(IMeeple meeple)
        {
            return meeple.MayMarry
                   && null != meeple.Player
                   && null != meeple.Player.Location
                   && meeple.Player.Location.CanMarryHere;
        }
        public override void Perform(IMeeple meeple)
        {
            meeple.Player.Marry();
        }
    }
}
