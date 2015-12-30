using System.Linq;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Interfaces;
using StIvesLib.Locations;
using StIvesLib.WorkActions;

namespace StIvesLib.Phases
{
    internal class Morning : Phase
    {
        public Morning(IGameInternal game)
            : base(game)
        {
            game.NextTurn();
        }

        public override IPhase NextPhase
        {
            get { return new Day(TheGame); }
        }

        public override PhaseType Type
        {
            get { return PhaseType.Morning; }
        }

        public override bool PerPlayer { get { return true; } }

        public override void Action(IPlayer player)
        {
            // For each token, if they're available, find out where they'll be placed
            foreach (var meeple in player.Meeples.Where(m => m.CanWork))
            {
                var actionRequest = new ActionRequest(player, meeple, ActionRequestType.WorkAssignment);

                foreach (var action in WorkAction.AllWorkActions.Where(a => a.CanPerform(meeple)))
                {
                    PerformAction actionResponse;
                    switch (action.Type)
                    {
                        case WorkActionType.Travel:
                            actionResponse = GetLocation;
                            break;
                        case WorkActionType.ListInns:
                            actionResponse = ShowInnPrices;
                            break;
                        default:
                            actionResponse = (response, target) => { response.Meeple.DayAction = target as WorkAction; };
                            break;
                    }
                    actionRequest.AddOption(action.Name, actionResponse, action);
                }
                SendRequest(actionRequest);
            }
        }

        public void GetLocation(ActionRequest response, object target)
        {
            if (null != response.Player.Location.NextLocation)
            {
                return;
            }

            var actionRequest = new ActionRequest(response.Player, response.Meeple, ActionRequestType.Location);
            foreach (var location in response.Player.Location.AvailableLocations.Except(new[] { response.Player.Location.Current }))
            {
                actionRequest.AddOption(location.Name, Move, location);
            }
            SendRequest(actionRequest);
        }

        public void ShowInnPrices(ActionRequest response, object target)
        {
            var actionRequest = new Actions.ShowInnPrices(response.Player);
            SendRequest(actionRequest);
            Action(response.Player);
        }

        public void Move(ActionRequest response, object target)
        {
            var travel = new Travel();
            response.Meeple.DayAction = travel;
            response.Player.Location.NextLocation = target as Location;
            var distance = response.Player.Location.DistanceToTravel;
            for (var i = 0; i < distance; i++)
            {
                travel.AddEvent(response.Player.Game.Events.NextEvent);
            }
        }
    }
}
