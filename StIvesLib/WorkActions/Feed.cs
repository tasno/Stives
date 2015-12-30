using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class Feed : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Feed; }
        }

        public override void Perform(IMeeple meeple)
        {
            meeple.Player.FeedCats(meeple.MaxKittens);
        }
    }
}
