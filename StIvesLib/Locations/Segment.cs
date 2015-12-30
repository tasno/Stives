using StIves.Interfaces;

namespace StIvesLib.Locations
{
    public class Segment : ISegment
    {
        public Segment()
        {
            Length = 1;
        }
        public ILocation Start { get; set; }
        public ILocation End { get; set; }
        public int Length { get; private set; }
        public static Segment operator +(Segment seg1, Segment seg2)
        {
            if (seg1.End.Equals(seg2.Start))
            {
                return (seg1.Start.Equals(seg2.End)) ? null : new Segment() { Start = seg1.Start, End = seg2.End, Length = seg1.Length + seg2.Length };
            }
            return null;
        }

        public ISegment Reverse()
        {
            return new Segment() { Start = End, End = Start, Length = Length };
        }

        public override string ToString()
        {
            return string.Format("From {0} to {1} is {2} miles.", Start.Name, End.Name, Length);
        }
    }
}
