using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class Catch : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Catch; }
        }

        public override void Perform(IMeeple meeple)
        {
            meeple.Player.CatchCats(meeple.MaxCats);
        }
    }
}
