using System;
using StIves.Interfaces;
using StIves.Interfaces.Actions;

namespace StIvesLib.Interfaces
{
    public interface IPhase
    {
        IPhase NextPhase { get; }
        string Description { get; }
        PhaseType Type { get; }
        bool PerPlayer { get; }
        void Action(IPlayer player);
        void Action();
        event EventHandler<ActionRequest> PlayerInputRequired;
        void PlayerInput(ActionRequest response);
    }
}
