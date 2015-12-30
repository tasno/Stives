using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;

namespace StIvesLib.Locations
{
    public class PlayerLocation : IPlayerLocation
    {
        public PlayerLocation(IGameLocations locations)
        {
            _gameLocations = locations;
            Current = locations.StartingLocation;
            _availableLocations = new List<ILocation>(locations.AllLocations);
            _availableWeddingLocations = new List<ILocation>(locations.AllLocations.Where(l => l.HasChurch));
        }

        public void MarryAt()
        {
            if (CanMarryHere)
            {
                _availableWeddingLocations.Remove(Current);
            }
        }

        public void BannedFrom(ILocation location)
        {
            _availableWeddingLocations.Remove(location);
            _availableLocations.Remove(location);
        }

        public ILocation Current { get; private set; }
        public IEnumerable<ILocation> AvailableLocations { get { return _availableLocations.Select(l => l); } }
        public IEnumerable<ILocation> AvailableWeddingLocations { get { return _availableWeddingLocations.Select(l => l); } }
        private readonly IList<ILocation> _availableLocations;
        private readonly IList<ILocation> _availableWeddingLocations;
        public ILocation NextLocation { get; set; }
        public int DistanceToTravel { get { return _gameLocations.Segments.GetDistance(Current, NextLocation); } }

        public int Move()
        {
            if (null == NextLocation)
                return 0;
            var distance = DistanceToTravel;
            Current = NextLocation;
            NextLocation = null;
            return distance;
        }

        public bool CanMarryHere
        {
            get
            {
                return AvailableWeddingLocations.Contains(Current);
            }
        }

        public override string ToString()
        {
            return Current.ToString();
        }

        private readonly IGameLocations _gameLocations;
    }
}
