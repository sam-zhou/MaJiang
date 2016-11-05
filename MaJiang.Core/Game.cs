using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public event EventHandler GameStarted;

        public event EventHandler<PlayerActionableEventArgs> PlayerActionable;
        public event EventHandler<PlayerWinEventArgs> PlayerWin;
        public event EventHandler<PlayerInitialWinEventArgs> PlayerInitalWin;
        public event EventHandler<PlayerDiscardEventArgs> PlayerDiscard;

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

                    foreach (PlayerDirection direction in Enum.GetValues(typeof(PlayerDirection)))
                    {
                        if (Players.All(q => q.PlayerDirection != direction))
                        {
                            player.SetDirection(direction);
                            Players.Add(player);
                            player.PlayerWin += PlayerOnPlayerWin;
                            player.PlayerActionable += PlayerOnPlayerActionable;
                            player.PlayerInitalWin += PlayerOnPlayerInitalWin;
                            player.PlayerDiscard += PlayerOnPlayerDiscard;

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

        private void PlayerOnPlayerDiscard(object sender, PlayerDiscardEventArgs e)
        {
            var player = sender as Player;
            if (player == null)
            {
                return;
            }

            AvailablePlayerActions = null;

            foreach (var otherPlayer in Players.Where(q => q.Id != player.Id))
            {
                otherPlayer.DiscardByOther(e.Tile, otherPlayer.PlayerDirection == player.PlayerDirection.GetNextDirection());
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

        private void PlayerOnPlayerWin(object sender, PlayerWinEventArgs e)
        {
            var palyer = sender as Player;
            if (palyer == null)
            {
                return;
            }

            foreach (var winningTile in e.WinningTiles)
            {
                Console.WriteLine(palyer.Name + " 可以胡: " + winningTile);
            }

        }

        private void PlayerOnPlayerActionable(object sender, PlayerActionableEventArgs e)
        {
            var player = sender as Player;
            if (player == null)
            {
                return;
            }

            var availablePlayerAction = new AvailablePlayerAction();
            availablePlayerAction.ActionOnTile = e.ActionOnTile;
            availablePlayerAction.Direction = player.PlayerDirection;
            availablePlayerAction.PlayerAction = e.PlayerAction;
            availablePlayerAction.Melds = e.Melds;

            AvailablePlayerActions.Add(availablePlayerAction);

            Console.WriteLine(player.Name + " 可以 " + e.PlayerAction.GetAttribute<DescriptionAttribute>().Description + " , 选择为: " + e.Melds.GetString());
        }

        private void IntialiseTilesOnHand()
        {
            var list = new List<List<Tile>>();



            for (int i = 0; i < 4; i++)
            {
                list.Add(new List<Tile>());
            }

            for (int i = 0; i < 4; i++)
            {
                list[i].AddRange(Board.GetNextTiles(4));
            }

            for (int i = 0; i < 4; i++)
            {
                list[i].AddRange(Board.GetNextTiles(4));
            }

            for (int i = 0; i < 4; i++)
            {
                list[i].AddRange(Board.GetNextTiles(4));
            }

            for (int i = 0; i < 4; i++)
            {
                list[i].AddRange(Board.GetNextTiles(1));
            }

            list[0].AddRange(Board.GetNextTiles(1));

            for (int i = 0; i < 4; i++)
            {
                Players[i].InitialDraw(list[i]);
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
