using StIves.Interfaces;

namespace StIvesLib.Inns
{
    public abstract class Bid
    {
        protected Bid(IPlayer player, int beds, int price)
        {
            Player = player;
            Beds = beds;
            Price = price;
        }
        public readonly IPlayer Player;
        public readonly int Beds;
        public readonly int Price;
    }
}
