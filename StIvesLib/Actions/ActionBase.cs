using System;
using System.Collections.Generic;
using StIves.Interfaces.Actions;

namespace StIvesLib.Actions
{
    public class ActionBase : IAction
    {
        protected ActionBase()
        {
            OutstandingActionRequests = new Dictionary<int, ActionRequest>();
        }

        public event EventHandler<ActionRequest> PlayerInputRequired;

        public SendRequest Send { get { return SendRequest; } }

        /// <summary>
        /// Returns false the message can't be queued.
        /// </summary>
        /// <param name="actionRequest"></param>
        /// <returns></returns>
        protected bool SendRequest(ActionRequest actionRequest)
        {
            if (null == actionRequest)
            {
                return true;
            }
            if (actionRequest.HasOptions)
            {
                if (OutstandingActionRequests.ContainsKey(actionRequest.GetHashCode()))
                {
                    return false;
                }
                OutstandingActionRequests.Add(actionRequest.GetHashCode(), actionRequest);
                GetPlayerInput(actionRequest);
            }
            else if (null != PlayerInputRequired)
            {
                PlayerInputRequired(this, actionRequest);
            }
            return true;
        }

        private void GetPlayerInput(ActionRequest actionRequest)
        {
            if (actionRequest.Player.ProcessAction(actionRequest))
            {
                PlayerInput(actionRequest);
            }
            else if (null != PlayerInputRequired)
            {
                PlayerInputRequired(this, actionRequest);
            }
        }

        public virtual void PlayerInput(ActionRequest response)
        {
            if (OutstandingActionRequests.ContainsKey(response.GetHashCode()))
            {
                OutstandingActionRequests.Remove(response.GetHashCode());
            }
            response.PerformResponse();
        }



        protected IDictionary<int, ActionRequest> OutstandingActionRequests;
    }

}
