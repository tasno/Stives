using System.Collections.Generic;
using System.Linq;
using StIves.Interfaces;
using StIvesLib.Locations;

namespace StIvesLib.Games
{
    public class GameSegments : IGameSegments
    {
        public GameSegments(GameLocations locations)
        {
            _locations = locations;

            var baseSegments = new[]
                {
                    new Segment() {Start = _locations.StIves, End = _locations.Houghton},
                    new Segment() {Start = _locations.StIves, End = _locations.Needingwoth},
                    new Segment() {Start = _locations.StIves, End = _locations.Fenstanton},
                    new Segment() {Start = _locations.StIves, End = _locations.HemingfordGrey},
                    new Segment() {Start = _locations.Needingwoth, End = _locations.Holywell},
                    new Segment() {Start = _locations.Fenstanton, End = _locations.FenDrayton},
                    new Segment() {Start = _locations.HemingfordGrey, End = _locations.HemingfordAbbots},
                };
            foreach (var baseSegment in baseSegments)
            {
                AddDistance(baseSegment);
            }
            var segmentsAdded = true;
            while (segmentsAdded)
            {
                segmentsAdded = false;
                var currentValues = new Segment[_distances.Count];
                _distances.Values.CopyTo(currentValues, 0);
                foreach (var baseSegment in baseSegments)
                {
                    foreach (var segment in currentValues.Where(s => s.End.Equals(baseSegment.Start)))
                    {
                        var newSegment = segment + baseSegment;
                        segmentsAdded |= AddDistance(newSegment);
                    }
                }
            }
        }
        private readonly GameLocations _locations;
        private readonly Dictionary<SegmentKey, ISegment> _distances = new Dictionary<SegmentKey, ISegment>();

        private bool AddDistance(ISegment segment)
        {
            if (null == segment)
            {
                return false;
            }
            var res1 = AddDistanceInner(segment);
            var res2 = AddDistanceInner(segment.Reverse());
            return res1 || res2;
        }

        private bool AddDistanceInner(ISegment segment)
        {
            var result = false;
            if (null != segment)
            {
                var newKey = new SegmentKey() { Start = segment.Start, End = segment.End };
                if (!_distances.ContainsKey(newKey))
                {
                    _distances.Add(newKey, segment);
                    result = true;
                }
            }
            return result;
        }

        public int GetDistance(ILocation from, ILocation to)
        {
            return _distances[new SegmentKey() { Start = from, End = to }].Length;
        }
    }
}
