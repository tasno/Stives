using System.ComponentModel;
using System.Linq;
using System;
using System.Collections.Generic;
using NMock2;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Meeples;

namespace UnitTests.Mocks
{
    abstract class MockPlayer : IPlayer
    {
        protected MockPlayer(Mockery mock)
        {
            var location = mock.NewMock<ILocation>();
            _location = mock.NewMock<IPlayerLocation>();
            Stub.On(_location).GetProperty("Current").Will(Return.Value(location));
            var husband = new Husband();
            Meeples = new IMeeple[] { husband };
        }

        public void ShowPrompt(ActionRequest prompt)
        {
        }

        public virtual bool ProcessAction(ActionRequest actionRequest)
        {
            return true;
        }

        public IEnumerable<IWife> SingleLadies
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IMeeple> Meeples { get; private set; }

        public void Affiance(IWife newWife)
        {
            throw new NotImplementedException();
        }

        public int Kittens
        {
            get { return 7; }
        }

        private readonly IPlayerLocation _location;
        public IPlayerLocation Location
        {
            get { return _location; }
        }

        public IGame Game
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Money { get; set; }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public void CatchCats(int cats)
        {
            throw new NotImplementedException();
        }

        public void Marry()
        {
            throw new NotImplementedException();
        }

        public bool HasFewestKittens
        {
            get { throw new NotImplementedException(); }
        }

        public void Earn(int money)
        {
            throw new NotImplementedException();
        }

        public void FeedCats(int kittens)
        {
            throw new NotImplementedException();
        }

        public void SewSacks(int sacks)
        {
            throw new NotImplementedException();
        }

        public void Spend(int amount)
        {
            throw new NotImplementedException();
        }

        public void AddWife(IWife wife)
        {
            Meeples = Meeples.Concat(new IMeeple[] { wife });
        }
        public void AddWives(IEnumerable<IWife> wives)
        {
            Meeples = Meeples.Concat(wives);
        }
        public int MaxBedsAt(int price)
        {
            return Math.Min(Money / price, Meeples.Count());
        }
    }
}
