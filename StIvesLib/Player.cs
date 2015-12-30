using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Exceptions;
using StIvesLib.Locations;
using StIvesLib.Meeples;
using Utils;

namespace StIvesLib
{
    public class Player : IPlayer
    {
        public Player(string name)
        {
            _name = name;
            Husband = new Husband() { Player = this };
            FianceeList = new List<IWife>();
            SingleLadiesList = new List<IWife>(Wife.NewWifeList);
            Money = 12;
        }

        public void Marry()
        {
            if (null == FianceeList || 0 == FianceeList.Count)
            {
                return;
            }
            Spend(6);
            var newWife = FianceeList.Pop();
            if (_wifeList.Count >= 7)
            {
                throw new TooManyWivesException();
            }
            _wifeList.Add(newWife);
            Earn(newWife.Dowry);
            Location.MarryAt();
        }

        public void Affiance(IWife newWife)
        {
            FianceeList.Add(newWife);
            SingleLadiesList.Remove(newWife);
            newWife.Player = this;
        }

        public void Spend(int amount)
        {
            if (amount > Money)
            {
                throw new OutOfMoneyException(amount, Money, Name);
            }
            Money -= amount;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendFormat("{0} is in {1}. He has {2}d, {3} wives, {4} sacks, {5} cats and {6} kittens.\n", Name,
                                Location.Current.Name, Money, WifeList.Count(), Sacks, Cats, Kittens);
            if (WifeList.Any())
            {
                result.Append("Married to: ");
                var firstwife = true;
                foreach (var wife in WifeList)
                {
                    if (firstwife)
                    {
                        firstwife = false;
                    }
                    else
                    {
                        result.Append("; ");
                    }
                    result.Append(wife.Name);
                }
                result.Append(".\n");
            }
            return result.ToString();
        }

        public IEnumerable<IWife> SingleLadies
        {
            get { return SingleLadiesList.Select(w => w); }
        }

        public IEnumerable<IMeeple> Meeples
        {
            get
            {
                yield return Husband;
                foreach (var wife in WifeList)
                {
                    yield return wife;
                }
            }
        }

        public string Name { get { return _name; } }
        private readonly string _name;

        public void SewSacks(int sacks)
        {
            Sacks += sacks;
        }

        public void CatchCats(int cats)
        {
            var maxCats = (int)Math.Ceiling((float)Sacks / 7);
            Cats += Math.Min(cats, maxCats);
        }

        public void FeedCats(int kittens)
        {
            var maxKits = (int)Math.Ceiling((float)Cats / 7);
            Kittens += Math.Min(kittens, maxKits);
        }

        public void Earn(int money)
        {
            Money += money;
        }

        public bool HasFewestKittens
        {
            get
            {
                return this == Game.PlayerWithFewestKittens;
            }
        }

        public int Money { get; private set; }
        protected int Sacks { get; private set; }
        protected int Cats { get; private set; }
        public int Kittens { get; private set; }
        protected IEnumerable<IWife> WifeList { get { return _wifeList.Take(7); } }
        private readonly IList<IWife> _wifeList = new List<IWife>();
        protected List<IWife> FianceeList { get; private set; }
        protected List<IWife> SingleLadiesList { get; private set; }
        protected Husband Husband;

        public IPlayerLocation Location { get; private set; }

        public IGame Game
        {
            get { return _game; }
            set
            {
                _game = value;
                Location = new PlayerLocation(value.Locations);
            }
        }

        private IGame _game;

        /// <summary>
        /// Return true iff the action has been processed
        /// Otherwise the PlayerInputRequired event will be fired.
        /// </summary>
        /// <returns></returns>
        public virtual bool ProcessAction(ActionRequest actionRequest)
        {
            return false;
        }

        public virtual void ShowPrompt(ActionRequest prompt)
        {
        }

        public int MaxBedsAt(int price)
        {
            return Math.Min(Money / price, Meeples.Count());
        }
    }
}
