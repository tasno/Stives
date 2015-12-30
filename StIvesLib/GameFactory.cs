using StIves.Interfaces;

namespace StIvesLib
{
    public class GameFactory
    {
        public IGame Create(Player player)
        {
            return new Game(player);
        }
    }
}
