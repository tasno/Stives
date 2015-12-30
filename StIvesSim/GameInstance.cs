using System;
using System.Linq;
using Players;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib;

namespace StIvesSim
{
    public class GameInstance
    {
        public void Start(int defaultPlayers)
        {
            var humanPlayer = new ConsolePlayer("Rupert");
            _game = new GameFactory().Create(humanPlayer);
            _game.NewPhase += game_NewPhase;
            _game.PlayerInputRequired += game_PlayerInputRequired;
            _game.Open(humanPlayer);

            var playerCount = defaultPlayers;
            for (; ; )
            {
                Console.WriteLine("How many computer players ({0})?", playerCount);
                var entered = Console.ReadLine();
                if (string.IsNullOrEmpty(entered))
                {
                    break;
                }
                if (Int32.TryParse(entered, out playerCount))
                {
                    break;
                }
            }
            for (var i = 0; i < playerCount; i++)
            {
                var newPlayer = new RandomPlayer(i + 1);
                _game.Join(newPlayer);
            }
            _game.Start(humanPlayer);
        }


        private void game_PlayerInputRequired(object sender, ActionRequest e)
        {
            e.Player.ShowPrompt(e);

            if (e.Options.Any())
            {
                e.Response = Console.ReadLine();
                _game.PlayerInput(e);
            }
        }

        private void game_NewPhase(object sender, EventArgs e)
        {
            Console.Write(sender);
            Console.WriteLine("X to end game, any other key for next Phase.");
            var input = Console.ReadLine();
            if (0 != string.Compare("X", input, true))
            {
                _game.NextPhase();
            }
        }

        private IGame _game;
    }
}
