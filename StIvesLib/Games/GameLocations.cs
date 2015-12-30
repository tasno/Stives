using System.Collections.Generic;
using System.Linq;
using System.Text;
using StIves.Interfaces;
using StIvesLib.Exceptions;
using StIvesLib.Interfaces;
using StIvesLib.Locations;

namespace StIvesLib.Games
{
    public class GameLocations : IGameLocations
    {
        internal GameLocations(IGameInternal game)
        {
            _game = game;
            _segments = new GameSegments(this);
        }

        public void SetupInns(IInnFactory factory)
        {
            _innOptions = new List<IInnOption>(factory.InnSet);
            foreach (var location in AllLocations)
            {
                SetInnOption(location);
                SetInnOption(location);
            }
        }

        public void SetInnOption(ILocation location)
        {
            var option = _innOptions[0];
            location.SetInnOptions(option);
            _innOptions.Remove(option);
            if (0 == _innOptions.Count)
            {
                throw new LastInnTileException();
            }
        }

        public IEnumerable<ILocation> LocationsWithPlayers(int minPlayersAtLocation = 1)
        {
            var locationsToReset = new Dictionary<ILocation, int>();
            foreach (var player in _game.Players)
            {
                if (locationsToReset.ContainsKey(player.Location.Current))
                {
                    locationsToReset[player.Location.Current]++;
                }
                else
                {
                    locationsToReset.Add(player.Location.Current, 1);
                }
            }
            return locationsToReset.Where(l => l.Value >= minPlayersAtLocation).Select(l => l.Key);
        }

        public Location StIves = new Locations.StIves();
        public ILocation StartingLocation { get { return StIves; } }
        public IEnumerable<ILocation> AllLocations
        {
            get
            {
                yield return StIves;
                yield return Houghton;
                yield return Needingwoth;
                yield return Holywell;
                yield return HemingfordAbbots;
                yield return HemingfordGrey;
                yield return Fenstanton;
                yield return FenDrayton;
            }
        }
        public readonly Location Houghton = new Location("Houghton");
        public readonly Location Needingwoth = new Location("Needingwoth");
        public readonly Location Holywell = new Location("Holywell");
        public readonly Location HemingfordAbbots = new Location("Hemingford Abbots");
        public readonly Location HemingfordGrey = new Location("Hemingford Grey");
        public readonly Location Fenstanton = new Location("Fenstanton");
        public readonly Location FenDrayton = new Location("Fen Drayton");

        public IGameSegments Segments
        {
            get { return _segments; }
        }

        private readonly GameSegments _segments;
        private IList<IInnOption> _innOptions;

        public string InnSummary
        {
            get
            {
                var result = new StringBuilder();
                foreach (var location in AllLocations)
                {
                    result.AppendLine(location.ToString());
                }
                return result.ToString();
            }
        }

        private readonly IGameInternal _game;
    }
}
