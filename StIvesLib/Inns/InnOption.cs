using StIves.Interfaces;

namespace StIvesLib.Inns
{
    public class InnOption : IInnOption
    {
        public InnOption(int minimumPrice, int numberOfBeds)
        {
            _minimumPrice = minimumPrice;
            _numberOfBeds = numberOfBeds;
        }

        public int MinimumPrice { get { return _minimumPrice; } }
        public int NumberOfBeds { get { return _numberOfBeds; } }
        private readonly int _minimumPrice;
        private readonly int _numberOfBeds;
    }

}
