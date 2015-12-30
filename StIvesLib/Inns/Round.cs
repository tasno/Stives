using System;
using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Inns.Bids;

namespace StIvesLib.Inns
{
    public class Round
    {
        public Round(int minPrice, int beds, IEnumerable<IPlayer> players)
        {
            _price = minPrice;
            _beds = beds;

            IPlayer firstPlayer = null;
            IPlayer prevPlayer = null;
            foreach (var player in players)
            {
                _playerCount++;
                if (null == firstPlayer)
                {
                    firstPlayer= player;
                }
                if (null != prevPlayer)
                {
                    _playerChain.Add(prevPlayer, player);
                }
                prevPlayer = player;
            }
            if (firstPlayer == prevPlayer)
            {
                _playerChain.Add(prevPlayer, null);
            }
            else
            {
                _playerChain.Add(prevPlayer, firstPlayer);
            }
        }

        public event EventHandler<EventArgs<BidRequest>> BidNeeded;
        public event EventHandler<EventArgs<IEnumerable<Bid>>> Complete;

        public void RequestBids()
        {
            RequestBid(_playerChain.First().Key);
        }

        public void RequestBid(IPlayer player)
        {
            var maxBeds = Math.Min(_beds - _bedsBid, player.MaxBedsAt(_price));
            var nextPlayer = _playerChain[player];
            var request = new BidRequest(player, nextPlayer, maxBeds, _beds, _price);
            for (var i = 0; i <= maxBeds; i++)
            {
                request.AddOption(string.Format("{0} bed{1}", i, i == 1 ? "" : "s"), Bid, i, i == maxBeds);
            }
            if (maxBeds < player.MaxBedsAt(_price)
                && _playerCount > 1) // can raise
            {
                request.AddOption("Raise", Raise, maxBeds + 1);
            }
            BidNeeded(this, new EventArgs<BidRequest>(request));
        }

        public void Raise(ActionRequest response, object target)
        {
            Restart();
            _price++;
            RequestBid(response.Player);
        }

        public void Bid(ActionRequest response, object target)
        {
            var bedsBid = (int)target;
            var bidResponse = response as BidRequest;
            _bidsRequested.Add(response.Player, new Beds(response.Player, bedsBid, _price));
            _bedsBid += bedsBid;
            if (_bidsRequested.Count >= _playerCount)
            {
                IsComplete = true;
                if (null != Complete)
                {
                    Complete(this, new EventArgs<IEnumerable<Bid>>(Bids));
                }
            }
            else
            {
                if (null != bidResponse && null != bidResponse.NextPlayer)
                {
                    RequestBid(bidResponse.NextPlayer);
                }
            }
        }

        private void Restart()
        {
            _bidsRequested.Clear();
            _bedsBid = 0;
        }

        public IEnumerable<Bid> Bids { get { return _bidsRequested.Values; } }
        public bool IsComplete { get; private set; }
        private readonly IDictionary<IPlayer, Bid> _bidsRequested = new Dictionary<IPlayer, Bid>();
        private int _price;
        private readonly int _beds;
        private readonly int _playerCount;
        private int _bedsBid;
        private readonly IDictionary<IPlayer, IPlayer> _playerChain = new Dictionary<IPlayer, IPlayer>();
    }
}
