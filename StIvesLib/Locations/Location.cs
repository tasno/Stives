using System.Text;
using StIves.Interfaces;

namespace StIvesLib.Locations
{
    public class Location : ILocation
    {
        public Location(string name)
        {
            _name = name;
        }
        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendFormat("{0}: ", Name);
            if (null != Tonight)
            {
                result.AppendFormat("Tonight: {0} beds @ {1}d. ", Tonight.NumberOfBeds, Tonight.MinimumPrice);
            }
            if (null != TomorrowNight)
            {
                result.AppendFormat("Tomorrow: {0} beds @ {1}d.", TomorrowNight.NumberOfBeds, TomorrowNight.MinimumPrice);
            }
            return result.ToString();
        }
        public string Name { get { return _name; } }
        private readonly string _name;
        public virtual bool HasChurch { get { return true; } }
        public IInnOption Tonight { get; private set; }
        public IInnOption TomorrowNight { get; private set; }
        public void SetInnOptions(IInnOption option)
        {
            Tonight = TomorrowNight;
            TomorrowNight = option;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            var locn = obj as Location;
            if (null == locn)
                return false;
            return locn.Name == Name;
        }
    }
}
