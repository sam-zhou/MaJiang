using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class Tile
    {
        protected bool Equals(Tile other)
        {
            return Equals(Suit, other.Suit) && Equals(Rank, other.Rank);
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
                return (Suit.Equals(p.Suit)) && (Rank.Equals(p.Rank));
            }
        }

        public override int GetHashCode()
        {
            return Suit.GetHashCode()*10 + Rank.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}{1} ", Rank, Suit);
        }

        public Tile(Suit suit, int rank)
        {
            Suit = suit;
            Rank = Rank.GetTileValue(rank);
        }


    }
}
