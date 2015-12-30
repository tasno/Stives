using System.Linq;
using NMock2;
using NUnit.Framework;
using StIves.Interfaces;
using StIvesLib.Inns;
using StIvesLib.Meeples;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestFixture]
    public class TestAuction
    {
        [Test]
        public void OnePlayerNoWifeEnoughRoomsAndMoney()
        {
            var mock = new Mockery();
            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(10));
            var player = new MockPlayerForBillet(mock, innOption)
            {
                Money = 2
            };
            player.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 1, Price = 2 });
            var players = new[] { player };
            var auction = new SynchronousAuction(player.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(1, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player, bid.Player);
        }
        [Test]
        public void OnePlayerEnoughRoomsAndMoney()
        {
            var mock = new Mockery();
            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(10));
            var player = new MockPlayerForBillet(mock, innOption)
            {
                Money = 16
            };
            player.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 8, Price = 2 });
            player.AddWives(Wife.NewWifeList);
            var players = new[] { player };
            var auction = new SynchronousAuction(player.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(8, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player, bid.Player);
        }
        [Test]
        public void OnePlayerNotEnoughRooms()
        {
            var mock = new Mockery();
            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(5));
            var player = new MockPlayerForBillet(mock, innOption)
            {
                Money = 10
            };
            player.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 5, Price = 2 });

            player.AddWives(Wife.NewWifeList);
            var players = new[] { player };
            var auction = new SynchronousAuction(player.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(5, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player, bid.Player);
        }
        [Test]
        public void OnePlayerNotEnoughMoney()
        {
            var mock = new Mockery();
            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(10));
            var player = new MockPlayerForBillet(mock, innOption)
            {
                Money = 4
            };
            player.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 2 });

            player.AddWives(Wife.NewWifeList);
            var players = new[] { player };
            var auction = new SynchronousAuction(player.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(2, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player, bid.Player);
        }
        [Test]
        public void OnePlayerDontTakeAll()
        {
            var mock = new Mockery();
            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(10));
            var player = new MockPlayerForBillet(mock, innOption)
            {
                Money = 4
            };

            player.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 2, Selection = 1 });
            player.AddWives(Wife.NewWifeList);
            var players = new[] { player };
            var auction = new SynchronousAuction(player.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(1, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player, bid.Player);
        }
        [Test]
        public void TwoPlayersNoRaise()
        {
            var mock = new Mockery();

            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(16));

            var player1 = new MockPlayerForBillet(mock, innOption)
            {
                Money = 4
            };
            player1.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 2 });
            player1.AddWives(Wife.NewWifeList);
            var player2 = new MockPlayerForBillet(mock, innOption)
            {
                Money = 6
            };
            player2.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 3, Price = 2 });
            player2.AddWives(Wife.NewWifeList);
            var players = new[] { player1, player2 };
            var auction = new SynchronousAuction(player1.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(2, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player1, bid.Player);
            bid = result.Last();
            Assert.IsNotNull(bid);
            Assert.AreEqual(3, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player2, bid.Player);
        }
        [Test]
        public void TwoPlayersSecondCanRaiseButDoesnt()
        {
            var mock = new Mockery();

            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(4));

            var player1 = new MockPlayerForBillet(mock, innOption)
            {
                Money = 4
            };
            player1.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 2 });
            player1.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 1, Price = 2 });
            player1.AddWives(Wife.NewWifeList);
            var player2 = new MockPlayerForBillet(mock, innOption)
            {
                Money = 6,
            };
            player2.AddBidResponse(new BidResponse() { CanRaise = true, MaxBeds = 2, Price = 2, Selection = 2 });
            player2.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 3 });
            player2.AddWives(Wife.NewWifeList);
            var players = new[] { player1, player2 };
            var auction = new SynchronousAuction(player1.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(2, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player1, bid.Player);
            bid = result.Last();
            Assert.IsNotNull(bid);
            Assert.AreEqual(2, bid.Beds);
            Assert.AreEqual(2, bid.Price);
            Assert.AreEqual(player2, bid.Player);
        }
        [Test]
        public void TwoPlayersSecondRaises()
        {
            var mock = new Mockery();

            var innOption = mock.NewMock<IInnOption>();
            Stub.On(innOption).GetProperty("MinimumPrice").Will(Return.Value(2));
            Stub.On(innOption).GetProperty("NumberOfBeds").Will(Return.Value(4));

            var player1 = new MockPlayerForBillet(mock, innOption)
            {
                Money = 4
            };
            player1.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 2 });
            player1.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 1, Price = 3 });
            player1.AddWives(Wife.NewWifeList);
            var player2 = new MockPlayerForBillet(mock, innOption)
            {
                Money = 6,
            };
            player2.AddBidResponse(new BidResponse() { CanRaise = true, MaxBeds = 2, Price = 2, Selection = 3 });
            player2.AddBidResponse(new BidResponse() { CanRaise = false, MaxBeds = 2, Price = 3 });
            player2.AddWives(Wife.NewWifeList);
            var players = new[] { player1, player2 };
            var auction = new SynchronousAuction(player1.Location.Current, players);
            var result = auction.Resolve();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            var bid = result.First();
            Assert.IsNotNull(bid);
            Assert.AreEqual(2, bid.Beds);
            Assert.AreEqual(3, bid.Price);
            Assert.AreEqual(player2, bid.Player);
            bid = result.Last();
            Assert.IsNotNull(bid);
            Assert.AreEqual(1, bid.Beds);
            Assert.AreEqual(3, bid.Price);
            Assert.AreEqual(player1, bid.Player);
        }
    }
}
