using System;
using StIves.Interfaces;

namespace StIvesLib.WorkActions
{
    class Cromwell : WorkAction
    {
        public override WorkActionType Type
        {
            get { return WorkActionType.Cromwell; }
        }
        public override bool CanPerform(IMeeple meeple)
        {
            return meeple.Player.HasFewestKittens;
        }
        public override void Perform(IMeeple meeple)
        {
            throw new NotImplementedException();
        }
    }
}
