using System.Collections.Generic;

namespace StIves.Interfaces
{
    public interface IGameLocations
    {
        void SetInnOption(ILocation location);
        IEnumerable<ILocation> LocationsWithPlayers(int minPlayersAtLocation = 1);
        IEnumerable<ILocation> AllLocations { get; }
        string InnSummary { get; }
        ILocation StartingLocation { get; }
        IGameSegments Segments { get; }
        void SetupInns(IInnFactory factory);
    }
}
