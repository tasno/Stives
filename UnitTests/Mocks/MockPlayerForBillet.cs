using System.Collections.Generic;
using System.Linq;
using NMock2;
using NUnit.Framework;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using Utils;

namespace UnitTests.Mocks
{
    class MockPlayerForBillet : MockPlayer
    {
        public MockPlayerForBillet(Mockery mock, IInnOption innOption)
            : base(mock)
        {
            Stub.On(Location.Current).GetProperty("Tonight").Will(Return.Value(innOption));
            Stub.On(Location.Current).Method("Equals").Will(Return.Value(true));
        }

        public override bool ProcessAction(ActionRequest actionRequest)
        {
            if (ActionRequestType.Billet == actionRequest.RequestType)
            {
                Assert.IsNotNull(actionRequest.Options);
                var expectedResponse = _expectedResponses.Pop();
                Assert.AreEqual(expectedResponse.OptionCount, actionRequest.Options.Count());  // +1 for 0 beds, +1 for Raise
                var i = 0;
                foreach (var option in actionRequest.Options)
                {
                    Assert.IsNotNull(option);
                    Assert.AreEqual(i, option.Target);
                    if (i + 1 == expectedResponse.OptionCount && expectedResponse.CanRaise)
                    {
                        Assert.AreEqual("Raise", option.Prompt);
                    }
                    else
                    {
                        Assert.IsTrue(option.Prompt.StartsWith(i.ToString()));
                    }
                    i++;
                }
                actionRequest.Response = expectedResponse.Response;
                return true;
            }
            return base.ProcessAction(actionRequest);
        }

        public void AddBidResponse(BidResponse response) { _expectedResponses.Add(response); }
        private readonly IList<BidResponse> _expectedResponses = new List<BidResponse>();
    }

    public class BidResponse
    {
        public virtual int OptionCount { get { return MaxBeds + (CanRaise ? 2 : 1); } }
        public virtual int MaxBeds { private get; set; }
        public int Price;
        public bool CanRaise;
        public int? Selection { private get; set; }
        public string Response { get { return Selection.HasValue ? Selection.ToString() : string.Empty; } }
    }
}
