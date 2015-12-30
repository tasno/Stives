using System.Collections.Generic;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Events;
using Utils;

namespace StIvesLib.Games
{
    public class GameEvents : IGameEvents
    {
        public GameEvents()
        {
            Initialise();
        }

        private void Initialise()
        {
            for (var i = 0; i < 4; i++)
            {
                _allEvents.Add(new UneventfulJourney());
            }
            for (var i = 0; i < 4; i++)
            {
                _allEvents.Add(new Highwayman());
            }
            _allEvents.Shuffle();
        }

        public IEvent NextEvent
        {
            get
            {
                var result = _allEvents.Pop();
                if (null == result)
                {
                    Initialise();
                    result = _allEvents.Pop();
                }
                return result;
            }
        }
        private readonly IList<EventBase> _allEvents = new List<EventBase>();
    }
}
