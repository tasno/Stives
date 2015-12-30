using StIves.Interfaces;
using StIves.Interfaces.Actions;

namespace StIvesLib.Events
{
    class UneventfulJourney : EventBase
    {
        public override string Name
        {
            get { return "Uneventful Journey"; }
        }

        public override string Prompt
        {
            get { return "Nothing happened"; }
        }

    }
}
