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

        private List<Player> _players; 

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

        public List<Player> Players
        {
            get
            {
                if (_players == null)
                {
                    _players = new List<Player>();
                    AddPlayer(new Player("Sam"));
                    AddPlayer(new Player("Micha"));
                    AddPlayer(new Player("JJ"));
                    AddPlayer(new Player("JiaWei"));
                }
                return _players;
            }
        }

        private void AddPlayer(Player player)
        {
            _players.Add(player);
            player.PlayerWin += PlayerOnPlayerWin;
            player.PlayerActionable += PlayerOnPlayerActionable;
            player.PlayerInitalWin += PlayerOnPlayerInitalWin;
        }

        private static void PlayerOnPlayerInitalWin(object sender, PlayerInitialWinEventArgs e)
        {
            var palyer = sender as Player;
            if (palyer == null)
            {
                return;
            }

            if (e.Type == InitialWinType.DaSiXi)
            {
                foreach (var meld in e.Melds)
                {
                    System.Console.WriteLine(palyer.Name + " 大四喜: " + meld.Tiles.First());
                }

            }
            else if (e.Type == InitialWinType.LiuLiuShun)
            {
                System.Console.Write(palyer.Name + " 六六顺: ");
                foreach (var meld in e.Melds)
                {
                    System.Console.Write(meld.Tiles.First());
                }
                System.Console.WriteLine();
            }
            else if (e.Type == InitialWinType.QueYiSe)
            {
                System.Console.Write(palyer.Name + " 缺一色: ");
                foreach (var suit in e.LackSuits)
                {
                    System.Console.Write(suit.GetAttribute<DescriptionAttribute>().Description);
                }
                System.Console.WriteLine();
            }
            else if (e.Type == InitialWinType.BanBanHu)
            {
                System.Console.Write(palyer.Name + " 板板胡");
            }
        }

        private static void PlayerOnPlayerWin(object sender, PlayerWinEventArgs e)
        {
            var palyer = sender as Player;
            if (palyer == null)
            {
                return;
            }

            foreach (var winningTile in e.WinningTiles)
            {
                System.Console.WriteLine(palyer.Name + " 可以胡: " + winningTile);
            }

        }

        private static void PlayerOnPlayerActionable(object sender, PlayerActionableEventArgs e)
        {
            var palyer = sender as Player;
            if (palyer == null)
            {
                return;
            }

            System.Console.WriteLine(palyer.Name + " 可以 " + e.PlayerAction.GetAttribute<DescriptionAttribute>().Description + " , 选择为: " + e.Melds.GetString());
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
                Players[i].TilesOnHand.InitialDraw(list[i]);
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
