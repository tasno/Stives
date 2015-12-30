using System.Collections.Generic;

namespace StIves.Interfaces
{
    public interface IInnFactory
    {
        IEnumerable<IInnOption> InnSet { get; }
    }
}