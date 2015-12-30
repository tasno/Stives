using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Inns;
using StIvesLib.Interfaces;

namespace StIvesLib.Phases
{
    internal class Evening : Phase
    {
        public Evening(IGameInternal game)
            : base(game)
        {
        }

        public override IPhase NextPhase
        {
            get { return new Night(TheGame); }
        }

        public override PhaseType Type
        {
            get { return PhaseType.Evening; }
        }

        public override void Action()
        {
            foreach (var location in TheGame.Locations.LocationsWithPlayers())
            {
                var auction = new Auction(location, TheGame.Players.Where(p => p.Location.Current.Equals(location)));
                auction.BidNeeded += auction_BidNeeded;
                auction.Complete += auction_Complete;
                auction.Resolve();
            }
        }

        void auction_Complete(object sender, EventArgs<IEnumerable<Bid>> e)
        {
            foreach (var bid in e.Data)
            {
                var i = 0;
                foreach (var meeple in bid.Player.Meeples)
                {
                    i++;
                    if (i > bid.Beds)
                    {
                        meeple.Billet(false);
                    }
                    else
                    {
                        meeple.Billet(true);
                        bid.Player.Spend(bid.Price);
                    }
                }
            }
        }

        void auction_BidNeeded(object sender, EventArgs<BidRequest> e)
        {
            SendRequest(e.Data);
        }
    }
}
