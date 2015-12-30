using System.Collections.Generic;
using StIves.Interfaces.Actions;

namespace StIves.Interfaces
{
    public interface IPlayer
    {
        /// <summary>
        /// Display the text to the player
        /// </summary>
        void ShowPrompt(ActionRequest prompt);
        /// <summary>
        /// Return true iff the action has been processed
        /// Otherwise the PlayerInputRequired event will be fired.
        /// </summary>
        /// <returns></returns>
        bool ProcessAction(ActionRequest actionRequest);
        IEnumerable<IWife> SingleLadies { get; }
        IEnumerable<IMeeple> Meeples { get; }
        void Affiance(IWife newWife);
        int Kittens { get; }
        IPlayerLocation Location { get; }
        IGame Game { get; set; }
        int Money { get; }
        string Name { get; }
        void CatchCats(int cats);
        void Marry();
        bool HasFewestKittens { get; }
        void Earn(int money);
        void FeedCats(int kittens);
        void SewSacks(int sacks);
        void Spend(int amount);
        int MaxBedsAt(int price);
    }
}
