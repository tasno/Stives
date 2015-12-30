using StIves.Interfaces;

namespace StIvesLib.Inns.Bids
{
    class Beds : Bid
    {
        public Beds(IPlayer player, int beds, int price)
            : base(player, beds, price)
        {
        }
    }
}
