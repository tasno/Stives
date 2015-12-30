using StIves.Interfaces;
using StIvesLib.Meeples.Wives;

namespace StIvesLib.Meeples
{
    public abstract class Wife : Meeple, IWife
    {
        public abstract int Dowry { get; }
        public override int FarmingIncome { get { return 6; } }
        public static Wife[] NewWifeList
        {
            get
            {
                return new Wife[]
                {
                    new Pioneer(), new Farmer(), new Barmaid(), new Witch(), new Seamstress(), new Catcher(), new Breeder(),  new Shooter(), new Matchmaker(), new Driver()
                };
            }
        }
    }
}
