using StIves.Interfaces;

namespace StIvesLib.Interfaces
{
    internal interface IMeepleInternal : IMeeple
    {
        string Name { get; }
        bool BilletAssigned { get; } // TBD
        int DefaultBillet { get; } // TBD
    }
}
