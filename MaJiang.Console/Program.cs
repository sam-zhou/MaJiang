using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        //    game.Reset();

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
            var input = string.Empty;
            var game = new Game();

            while (input == string.Empty)
            {
               

                game.Reset();

                //foreach (var player in game.Players)
                //{
                //    System.Console.WriteLine(player.ToString());
                //}
                input = System.Console.ReadLine();
            }


            
        }



        //static void Main(string[] args)
        //{
        //    var list = new List<Tile>
        //    {
        //        new Tile(Suit.Bamboo, 1),
        //        new Tile(Suit.Bamboo, 2),
        //        new Tile(Suit.Bamboo, 3)
        //    }.GetChildSets();

        //    foreach (var item in list)
        //    {
        //        foreach (var tile in item)
        //        {
        //            System.Console.Write(tile);
        //        }

        //        System.Console.WriteLine();
        //    }

        //    System.Console.ReadLine();
        //}
    }
}
