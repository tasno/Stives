using System;
using System.Linq;

namespace StIvesLib.Events
{
    public class Highwayman : EventBase
    {
        public override string Name
        {
            get { return "Highwayman"; }
        }

        public override string Prompt
        {
            get { return _prompt; }
        }

        public override StIves.Interfaces.Actions.ActionRequest GetActionRequest(StIves.Interfaces.IPlayer player)
        {
            if (player.Money <= 12)
            {
                _prompt = "You did not have enough money to attract his attention";
            }
            else if (player.Meeples.Any(m => m.ShootsFirst))
            {
                _prompt = "One of your meeples shot first";
            }
            else
            {
                var amountToLose = (int)Math.Floor((decimal)(player.Money) / 2);
                player.Spend(amountToLose);
                _prompt = string.Format("He stole {0}d. You have {1}d left", amountToLose, player.Money);
            }
            return base.GetActionRequest(player);
        }

        private string _prompt;
    }
}
