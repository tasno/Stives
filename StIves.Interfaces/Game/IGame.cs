using System;
using System.Collections.Generic;
using StIves.Interfaces.Actions;

namespace StIves.Interfaces
{
    public interface IGame
    {
        void Join(IPlayer player);
        void Start(IPlayer player);
        void Open(IPlayer player);
        void NextPhase();
        event EventHandler NewPhase;
        event EventHandler<ActionRequest> PlayerInputRequired;
        void PlayerInput(ActionRequest response);
        IPlayer PlayerWithFewestKittens { get; }
        IGameLocations Locations { get; }
        IGameEvents Events { get; }
        IList<IPlayer> Players { get; }
        int TurnNumber { get; }
    }
}