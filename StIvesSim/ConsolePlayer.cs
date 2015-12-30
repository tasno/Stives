using System;
using StIves.Interfaces.Actions;
using StIvesLib;

namespace StIvesSim
{
    public class ConsolePlayer : Player
    {
        public ConsolePlayer(string name)
            : base(name)
        {
        }

        public override void ShowPrompt(ActionRequest e)
        {
            if (!string.IsNullOrEmpty(e.Prompt))
            {
                Console.WriteLine("{0}: {1}", e.Player.Name, e.Prompt);
            }
            var i = 0;
            var defaultOption = 0;
            foreach (var option in e.Options)
            {
                if (option.IsDefault)
                {
                    defaultOption = i;
                }
                if (0 < i)
                {
                    Console.Write("; ");
                }
                Console.Write("{0}: {1}", i, option.Prompt);
                i++;
            }
            if (0 < i)
            {
                Console.Write("?\nDefault is {0}.\n", defaultOption);
            }
        }
    }
}
