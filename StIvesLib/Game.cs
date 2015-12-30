using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StIves.Interfaces;
using StIves.Interfaces.Actions;
using StIvesLib.Games;
using StIvesLib.Inns;
using StIvesLib.Interfaces;
using StIvesLib.Phases;

namespace StIvesLib
{
    internal class Game : IGameInternal
    {
        public Game(IPlayer player)
        {
            TurnNumber = 0;
            _locations = new GameLocations(this);
            _events = new GameEvents();
            CurrentPhase = new NotStarted(this);
            _players = new List<IPlayer>();
            _startingPlayer = player;
        }

        private void AddPlayer(IPlayer player)
        {
            Players.Add(player);
            player.Game = this;
            PlayerAction(player);
        }

        public void Join(IPlayer player)
        {
            if (IsOpen)
            {
                AddPlayer(player);
            }
        }

        public void Start(IPlayer player)
        {
            if (player == _startingPlayer)
            {
                _innFactory = new InnFactory(Players.Count);
                Locations.SetupInns(_innFactory);
                CurrentPhase.Action();
                NextPhase();
            }
        }

        public void Open(IPlayer player)
        {
            if (player == _startingPlayer)
            {
                IsOpen = true;
                Join(player);
            }
        }

        private bool IsOpen { get; set; }

        public IList<IPlayer> Players { get { return _players; } }
        private readonly IList<IPlayer> _players;
        private readonly IPlayer _startingPlayer;

        public event EventHandler NewPhase;
        public event EventHandler<ActionRequest> PlayerInputRequired;
        public void PlayerInput(ActionRequest response)
        {
            CurrentPhase.PlayerInput(response);
        }

        public void NextPhase()
        {
            CurrentPhase = CurrentPhase.NextPhase;
            PlayerActions();
            if (null != NewPhase)
            {
                NewPhase(this, new EventArgs());
            }
        }

        private void PlayerActions()
        {
            if (CurrentPhase.PerPlayer)
            {
                foreach (var player in Players)
                {
                    CurrentPhase.Action(player);
                }
            }
            else
            {
                CurrentPhase.Action();
            }
        }

        private void PlayerAction(IPlayer player)
        {
            CurrentPhase.Action(player);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine(CurrentPhase.ToString());
            foreach (var player in Players)
            {
                result.Append(player.ToString());
            }
            return result.ToString();
        }

        public IEnumerable<IPlayer> PlayersWithMostKittens
        {
            get { return Players.Where(p => p.Kittens == MostKittens); }
        }
        public int MostKittens
        {
            get { return Players.Max(p => p.Kittens); }
        }
        public IPlayer PlayerWithFewestKittens
        {
            get
            {
                return Players.FirstOrDefault(p => p.Kittens == FewestKittens);
            }
        }
        public int FewestKittens
        {
            get { return Players.Min(p => p.Kittens); }
        }

        public int TurnNumber { get; private set; }
        public void NextTurn()
        {
            TurnNumber++;
        }

        private IPhase _currentPhase;
        private IPhase CurrentPhase
        {
            get { return _currentPhase; }
            set
            {
                _currentPhase = value;
                _currentPhase.PlayerInputRequired += _currentPhase_PlayerInputRequired;
            }
        }

        void _currentPhase_PlayerInputRequired(object sender, ActionRequest e)
        {
            // pass on the event to our listeners
            if (null != PlayerInputRequired)
            {
                PlayerInputRequired(sender, e);
            }
        }

        public IGameLocations Locations { get { return _locations; } }
        public IGameEvents Events { get { return _events; } }
        private readonly IGameLocations _locations;
        private readonly IGameEvents _events;
        private InnFactory _innFactory;
    }
}
