using System.Collections.Generic;
using System.Threading;
using StIves.Interfaces;
using StIvesLib.Inns;

namespace UnitTests
{
    class SynchronousAuction : Auction
    {
        public SynchronousAuction(ILocation location, IEnumerable<IPlayer> players) :
            base(location, players)
        {
            BidNeeded += SynchronousAuction_BidNeeded;
            Complete += SynchronousAuction_Complete;
        }

        void SynchronousAuction_Complete(object sender, EventArgs<IEnumerable<Bid>> e)
        {
            _result = e.Data;
            Interlocked.Increment(ref _auctionThreadStop);
        }

        void SynchronousAuction_BidNeeded(object sender, EventArgs<BidRequest> e)
        {
            if (e.Data.Player.ProcessAction(e.Data))
            {
                e.Data.PerformResponse();
            }
        }

        /// <summary>
        /// For testing
        /// </summary>
        public new IEnumerable<Bid> Resolve()
        {
            var auctionThread = new Thread(Round.RequestBids)
                {
                    IsBackground = true
                };
            auctionThread.Start();
            while (0 == Interlocked.Read(ref _auctionThreadStop))
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(100);
            auctionThread.Abort();
            return _result;
        }

        private long _auctionThreadStop ;
        private IEnumerable<Bid> _result;
    }
}
