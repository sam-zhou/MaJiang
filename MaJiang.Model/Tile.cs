using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Extention;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class Tile
    {
        protected bool Equals(Tile other)
        {
            return Suit == other.Suit && Rank == other.Rank;
        }

        public Suit Suit { get; set; }

        public Rank Rank { get; set; }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                var p = (Tile)obj;
                return (Suit == p.Suit) && (Rank == p.Rank);
            }
        }

        public override int GetHashCode()
        {
            return Suit.GetHashCode()*10 + (int)Rank;
        }

        public override string ToString()
        {
            return string.Format("{0}{1} ", Rank.GetAttribute<DescriptionAttribute>().Description, Suit.GetAttribute<DescriptionAttribute>().Description);
        }

        public Tile(Suit suit, int rank)
        {
            Suit = suit;
            Rank = (Rank)rank;
        }

        public static Tile GetTile(Suit suit, int rank)
        {
            return TilesDictionary[suit][rank];
        }

        private static Dictionary<Suit, Dictionary<int, Tile>> _tilesDictionary;
        public static Dictionary<Suit, Dictionary<int, Tile>> TilesDictionary
        {
            get
            {
                if (_tilesDictionary == null)
                {
                    _tilesDictionary = new Dictionary<Suit, Dictionary<int, Tile>>();

                    var bambooDic = new Dictionary<int, Tile>();
                    var characterDic = new Dictionary<int, Tile>();
                    var dotDic = new Dictionary<int, Tile>();

                    for (int i = 1; i <= 9; i++)
                    {
                        var bamboo = new Tile(Suit.Bamboo, i);
                        var character = new Tile(Suit.Character, i);
                        var dot = new Tile(Suit.Dot, i);
                        
                        bambooDic.Add(i, bamboo);
                        characterDic.Add(i, character);
                        dotDic.Add(i, dot);
                    }

                    _tilesDictionary.Add(Suit.Bamboo, bambooDic);
                    _tilesDictionary.Add(Suit.Character, characterDic);
                    _tilesDictionary.Add(Suit.Dot, dotDic);
                }
                return _tilesDictionary;
            }
        }
    }
}
