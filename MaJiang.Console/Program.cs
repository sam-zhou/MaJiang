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
                input = "1";


                var count = 0;
                while (input != string.Empty)
                {
                    foreach (var player in game.Players)
                    {
                        System.Console.WriteLine(player.ToString());
                    }

                    System.Console.WriteLine("{0}输入出牌：", game.Players[count].Name);
                    input = System.Console.ReadLine();

                    int rank;
                    Suit suit = Suit.Bamboo;
                    if (input.ToLower()[0] == 'b')
                    {
                        suit = Suit.Bamboo;
                    }
                    else if (input.ToLower()[0] == 'c')
                    {
                        suit = Suit.Character;
                    }
                    else if (input.ToLower()[0] == 'd')
                    {
                        suit = Suit.Dot;
                    }

                    switch (input[1])
                    {
                        case '1':
                            rank = 1;
                            break;
                        case '2':
                            rank = 2;
                            break;
                        case '3':
                            rank = 3;
                            break;
                        case '4':
                            rank = 4;
                            break;
                        case '5':
                            rank = 5;
                            break;
                        case '6':
                            rank = 6;
                            break;
                        case '7':
                            rank = 7;
                            break;
                        case '8':
                            rank = 8;
                            break;
                        case '9':
                            rank = 9;
                            break;
                        default:
                            rank = 1;
                            break;
                    }

                    game.Players[0].Discard(new Tile(suit, rank));


                    count ++;
                    count = count%4;
                }
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
