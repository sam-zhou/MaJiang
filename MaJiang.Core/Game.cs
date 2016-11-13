using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using MaJiang.Extention;
using MaJiang.Model;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;

namespace MaJiang.Core
{
    public class Game
    {
        private Board _board;

        private List<AvailablePlayerAction> _availablePlayerActions;

        private List<Player> _players;

        private PlayerPositions _currentPlayerPosition;

        public event EventHandler GameStarted;

        public event EventHandler<PlayerActionEventArgs> PlayerAction;
        public event EventHandler<PlayerActionEventArgs> PlayerActionable;
        public event EventHandler<PlayerWinEventArgs> PlayerWinable;
        public event EventHandler<PlayerInitialWinEventArgs> PlayerInitalWin;

        public Board Board
        {
            get
            {
                if (_board == null)
                {
                    _board = new Board();
                }
                return _board;
            }
        }

        public bool IsBusy { get; private set; }

        public List<Player> Players
        {
            get
            {
                if (_players == null)
                {
                    _players = new List<Player>();
                    
                }
                return _players;
            }
        }

        private List<AvailablePlayerAction> AvailablePlayerActions
        {
            get
            {
                if (_availablePlayerActions == null)
                {
                    _availablePlayerActions = new List<AvailablePlayerAction>();
                }
                return _availablePlayerActions;
            }
            set { _availablePlayerActions = value; }
        }

        public void Join(Player player)
        {
            if (!IsBusy)
            {
                

                if (Players.Count < 4)
                {

                    foreach (PlayerPositions direction in Enum.GetValues(typeof(PlayerPositions)))
                    {
                        if (Players.All(q => q.PlayerPosition != direction))
                        {
                            player.SetDirection(direction);

                            if (Players.Count == 0)
                            {
                                player.IsDealer = true;
                                _currentPlayerPosition = direction;
                            }

                            Players.Add(player);
                            player.PlayerWinable += PlayerOnPlayerWinable;
                            player.PlayerActionable += PlayerOnPlayerActionable;
                            player.PlayerInitalWin += PlayerOnPlayerInitalWin;
                            player.PlayerAction += PlayerOnPlayerAction;
                            break;
                        }
                    }
                }


                if (Players.Count == 4)
                {
                    IsBusy = true;
                    Reset();

                    if (GameStarted != null)
                    {
                        GameStarted(this, null);
                    }
                }
            }
        }

        private void PlayerOnPlayerAction(object sender, PlayerActionEventArgs e)
        {
            var player = sender as Player;
            if (player == null)
            {
                return;
            }

            
            AvailablePlayerActions = null;

            if (e.PlayerAction == PlayerActions.Discard)
            {
                foreach (var otherPlayer in Players.Where(q => q.Id != player.Id))
                {
                    otherPlayer.DiscardByOther(e.ActionOnTile, otherPlayer.PlayerPosition == player.PlayerPosition.GetNextPosition());
                }
            }
            

            if (PlayerAction != null)
            {
                PlayerAction(player, e);
            }
        }

        private void PlayerOnPlayerInitalWin(object sender, PlayerInitialWinEventArgs e)
        {
            var palyer = sender as Player;
            if (palyer == null)
            {
                return;
            }

            if (PlayerInitalWin != null)
            {
                PlayerInitalWin(sender, e);
            }
        }

        private void PlayerOnPlayerWinable(object sender, PlayerWinEventArgs e)
        {
            var palyer = sender as Player;
            if (palyer == null)
            {
                return;
            }

            if (PlayerWinable != null)
            {
                PlayerWinable(sender, e);
            }
        }

        private void PlayerOnPlayerActionable(object sender, PlayerActionEventArgs e)
        {
            var player = sender as Player;
            if (player == null)
            {
                return;
            }

            var availablePlayerAction = new AvailablePlayerAction(e.PlayerAction, _currentPlayerPosition, player.PlayerPosition, e.ActionOnTile, e.Melds);

            AvailablePlayerActions.Add(availablePlayerAction);

            if (PlayerActionable != null)
            {
                PlayerActionable(sender, e);
            }
        }

        private Player GetPlayer(PlayerPositions position)
        {
            return Players.FirstOrDefault(q => q.PlayerPosition == position);
        }

        private Player GetCurrentPlayer()
        {
            return Players.FirstOrDefault(q => q.PlayerPosition == _currentPlayerPosition);
        }

        private void IntialiseTilesOnHand()
        {

            var dealer = GetCurrentPlayer();
            if (dealer != null)
            {
                for (var i = 0; i < 4; i++)
                {
                    var numberOfTiles = i == 3 ? 1 : 4;

                    dealer.InitialDraw(Board.GetNextTiles(numberOfTiles));

                    var position = dealer.PlayerPosition;
                    for (int j = 0; j < 3; j++)
                    {
                        position = position.GetNextPosition();

                        var player = GetPlayer(position);

                        if (player != null)
                        {
                            player.InitialDraw(Board.GetNextTiles(numberOfTiles));
                        }
                        else
                        {
                            throw new Exception("Cannot find player on " + position);
                        }
                    }
                }

                dealer.InitialDraw(Board.GetNextTiles(1));
            }
            else
            {
                throw new Exception("Cannot find dealer");
            }
        }

        public void Reset()
        {
            Board.Shuffle();
            foreach (var player in Players)
            {
                player.Reset();
            }
            IntialiseTilesOnHand();
        }
    }
}
