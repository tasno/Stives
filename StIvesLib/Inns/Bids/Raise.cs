using StIves.Interfaces;

namespace StIvesLib.Inns.Bids
{
    class Raise : Bid
    {
        public Raise(IPlayer player, int price)
            : base(player, 0, price)
        {
        }
    }
}
