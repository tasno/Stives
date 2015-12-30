namespace StIves.Interfaces
{
    public interface IMeeple
    {
        bool CanWork { get; }
        int FarmingIncome { get; }
        int MaxSacks { get; }
        int MaxCats { get; }
        int MaxKittens { get; }
        bool MayDrive { get; }
        bool MayMarry { get; }
        IPlayer Player { get; set; }
        IWorkAction DayAction { get; set; }
        bool Travelling { get; }
        void ResetBillet();
        void Billet(bool atInn);
        string Name { get; }
        bool ShootsFirst { get; }
    }
}

