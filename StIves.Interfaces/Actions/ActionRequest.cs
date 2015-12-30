using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace StIves.Interfaces.Actions
{

    [Serializable]
    public class ActionRequest : EventArgs
    {
        public ActionRequest()
        {
        }

        public ActionRequest(IPlayer player, IMeeple meeple, ActionRequestType requestType, string prompt = null)
        {
            Player = player;
            Meeple = meeple;
            RequestType = requestType;
            _prompt = prompt;
        }

        public override int GetHashCode()
        {
            var result = 0;
            if (null != Player)
                result += Player.GetHashCode();
            if (null != Meeple)
                result += Meeple.GetHashCode();
            result += RequestType.GetHashCode();
            result += Options.Sum(o => o.GetHashCode());
            return result;
        }

        public void AddOption(string prompt, PerformAction action, object target, bool isDefault = false)
        {
            var option = new Option(prompt, action, target, isDefault);
            _options.Add(option);
        }

        public void PerformResponse()
        {
            int action;
            if (!HasOptions)
                return;
            if (!Int32.TryParse(Response, out action))
            {
                var defaultOption = Options.FirstOrDefault(o => o.IsDefault);
                action = (null == defaultOption || null == defaultOption.Target) ? 0 : (int)defaultOption.Target;
            }
            PerformResponseAction(action);
        }

        public void PerformResponseAction(int response)
        {
            var option = _options[response];
            option.Action(this, option.Target);
        }

        public readonly IPlayer Player;
        public readonly IMeeple Meeple;

        public string Prompt
        {
            get { return _prompt ?? string.Format(EnumDescription<ActionRequestType>.GetDescription(RequestType), Meeple); }
        }

        public bool HasOptions { get { return _options.Count > 0; } }
        public string Response;
        public readonly ActionRequestType RequestType;
        public IEnumerable<Option> Options { get { return _options.Select(o => o); } }
        private readonly List<Option> _options = new List<Option>();
        private readonly string _prompt;
    }
}
