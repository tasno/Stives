using System;
using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;

namespace StIvesLib.Inns
{
    public class Auction
    {
        public Auction(ILocation location, IEnumerable<IPlayer> players)
        {
            Location = location;
            Players = players;
        }

        public event EventHandler<EventArgs<IEnumerable<Bid>>> Complete;
        public event EventHandler<EventArgs<BidRequest>> BidNeeded;

        public virtual void Resolve()
        {
            Round.RequestBids();
        }

        private Round _round;
        protected Round Round
        {
            get
            {
                if (null == _round)
                {
                    var playersInAuction =
                        Players.Where(p => p.Location.Current.Equals(Location)).OrderBy(p => -p.Kittens);
                    _round = new Round(Location.Tonight.MinimumPrice, Location.Tonight.NumberOfBeds, playersInAuction);
                    _round.BidNeeded += round_BidNeeded;
                    _round.Complete += round_Complete;
                }
                return _round;
            }
        }

        protected void round_Complete(object sender, EventArgs<IEnumerable<Bid>> e)
        {
            if (null != Complete)
            {
                Complete(sender, e);
            }
        }

        protected void round_BidNeeded(object sender, EventArgs<BidRequest> e)
        {
            if (null != BidNeeded)
            {
                BidNeeded(sender, e);
            }
        }


        public readonly ILocation Location;
        public readonly IEnumerable<IPlayer> Players;
    }

}
