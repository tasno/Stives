using System.Collections.Generic;

namespace StIves.Interfaces
{
    public interface IPlayerLocation
    {
        void MarryAt();
        void BannedFrom(ILocation location);
        ILocation Current { get; }
        IEnumerable<ILocation> AvailableLocations { get; }
        IEnumerable<ILocation> AvailableWeddingLocations { get; }
        ILocation NextLocation { get; set; }
        int DistanceToTravel { get; }
        bool CanMarryHere { get; }
        int Move();
    }
}