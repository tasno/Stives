namespace StIves.Interfaces
{
    public interface ISegment
    {
        ILocation Start { get; set; }
        ILocation End { get; set; }
        int Length { get; }
        ISegment Reverse();
    }
}