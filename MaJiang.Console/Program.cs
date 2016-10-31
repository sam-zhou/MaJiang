using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Core;
using MaJiang.Extention;
using MaJiang.Model;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;

namespace MaJiang.Console
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var game = new Game();
            
        //    game.Shuffle();

        //    System.Console.WriteLine(game.Board.ToString());

        //    game.Initialise();

        //    var tile = game.Board.GetNextTile();

        //    game.Players[0].TilesOnHand.Draw(tile);

        //    foreach (var player in game.Players)
        //    {
        //        System.Console.WriteLine(player.ToString());
        //    }

        //    foreach (var stack in game.Board.StacksCollection.Stacks)
        //    {
        //        System.Console.WriteLine(stack.ToString());
        //    }

        //    System.Console.ReadLine();
        //}

        static void Main(string[] args)
        {
            var player = new Player("Sam");

            player.TilesOnHand.InitialDraw(new List<Tile>
            {
                new Tile(Suit.Bamboo, 1),
                new Tile(Suit.Bamboo, 1),
                new Tile(Suit.Bamboo, 1),
                new Tile(Suit.Dot, 2),
                new Tile(Suit.Dot, 2),
                new Tile(Suit.Dot, 2),
                new Tile(Suit.Bamboo, 3),
                new Tile(Suit.Bamboo, 3),
                new Tile(Suit.Bamboo, 3),
                new Tile(Suit.Bamboo, 4),
                new Tile(Suit.Bamboo, 4),
                new Tile(Suit.Bamboo, 5),
                new Tile(Suit.Bamboo, 5),
            });

            
            player.PlayerWin += PlayerOnPlayerWin;
            player.PlayerActionable += PlayerOnPlayerActionable;
            player.DiscardByOther(new Tile(Suit.Bamboo, 5));

            System.Console.WriteLine(player.ToString());

            

            System.Console.ReadLine();
        }

        private static void PlayerOnPlayerWin(object sender, PlayerWinEventArgs e)
        {
            foreach (var winningTile in e.WinningTiles)
            {
                System.Console.WriteLine("WinningTile: " + winningTile);
            }
            
        }

        private static void PlayerOnPlayerActionable(object sender, PlayerActionableEventArgs e)
        {
            System.Console.WriteLine("Actionable: " + e.PlayerAction + " , Meld: " + e.Melds.GetString());
        }
    }
}
