namespace StIves.Interfaces
{
    public interface ILocation
    {
        bool HasChurch { get; }
        int GetHashCode();
        bool Equals(object obj);
        string Name { get; }
        void SetInnOptions(IInnOption option);
        IInnOption Tonight { get; }
        IInnOption TomorrowNight { get; }
    }
}