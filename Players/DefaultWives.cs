using StIves.Interfaces.Actions;
using StIvesLib;

namespace Players
{
    public class DefaultWives : Player
    {
        public DefaultWives(string name)
            : base(name)
        { }
        public override bool ProcessAction(ActionRequest actionRequest)
        {
            if (actionRequest.RequestType == ActionRequestType.Fiancee)
            {
                actionRequest.Response = string.Empty;  // default
                return true;
            }
            return base.ProcessAction(actionRequest);
        }
    }
}
