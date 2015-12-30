using System.Collections.Generic;
using StIves.Interfaces;

namespace StIvesLib.Interfaces
{
    internal interface IGameInternal : IGame
    {
        IEnumerable<IPlayer> PlayersWithMostKittens { get; } /// TBD
        int MostKittens { get; } /// TBD
        int FewestKittens { get; } /// TBD
        int TurnNumber { get; }
        void NextTurn();
    }
}
