using System;
using System.Linq;
using StIves.Interfaces;
using StIves.Interfaces.Actions;

namespace StIvesLib.Inns
{
    [Serializable]
    public class BidRequest : ActionRequest
    {
        public BidRequest(IPlayer player, IPlayer nextPlayer, int availableBeds, int totalBeds, int price)
            : base(player, null, ActionRequestType.Billet,
                string.Format("There are {0} of {4} beds available at {1}d. You have {2} people in your party and {3}d to spend.", availableBeds, price, player.Meeples.Count(), player.Money, totalBeds))
        {
            NextPlayer = nextPlayer;
        }

        public readonly IPlayer NextPlayer;
    }
}
