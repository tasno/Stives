using StIves.Interfaces;
using StIves.Interfaces.Actions;

namespace StIvesLib.Actions
{
    public class ShowInnPrices : ActionRequest
    {
        public ShowInnPrices(IPlayer player)
            : base(player, null, ActionRequestType.ShowInnPrices, player.Game.Locations.InnSummary)
        {

        }
    }
}
