using System;
using System.Linq;
using StIves.Interfaces.Actions;
using StIvesLib;

namespace Players
{
    public class RandomPlayer : Player
    {
        public RandomPlayer(int playerId)
            : base(string.Format("Bot #{0}", playerId))
        {
        }

        public override bool ProcessAction(ActionRequest actionRequest)
        {
            var option = Random.Next(actionRequest.Options.Count());
            actionRequest.Response = string.Format("{0}", option);
            return true;
        }

        private static readonly Random Random = new Random();
    }
}
