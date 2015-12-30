using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Interfaces;
using StIvesLib.Meeples;

namespace StIvesLib.Phases
{
    internal class NotStarted : Phase
    {
        internal NotStarted(IGameInternal game)
            : base(game)
        {
        }

        public override IPhase NextPhase
        {
            get { return new Morning(TheGame); }
        }

        public override PhaseType Type
        {
            get { return PhaseType.NotStarted; }
        }

        public override bool PerPlayer { get { return true; } }

        public override void Action(IPlayer player)
        {
            var actionRequest = new ActionRequest(player, null, ActionRequestType.Fiancee);
            foreach (var singleLady in player.SingleLadies)
            {
                actionRequest.AddOption(singleLady.Name, (response, target) => response.Player.Affiance(target as Wife), singleLady);
            }
            if (actionRequest.HasOptions)
            {
                SendRequest(actionRequest);
            }
        }

        public override void Action()
        {
            foreach (var actionRequest in TheGame.Players.Select(p => new Actions.ShowInnPrices(p)))
            {
                SendRequest(actionRequest);
            }
        }

        public override void PlayerInput(ActionRequest response)
        {
            base.PlayerInput(response);
            Action(response.Player);
        }
    }
}
