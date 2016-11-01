using System.Collections.Generic;
using System.Linq;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core
{
    public static class MaJiangAlgorithm
    {
        public static List<Meld> GetKongableMelds(IEnumerable<Tile> tiles, Tile lastDraw)
        {
            var output = new List<Meld>();

            var list = tiles.ToList();

            list.Add(lastDraw);

            list = list.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();

            foreach (var item in list.Distinct())
            {
                var count = list.Count(q => q.Equals(item));

                if (count == 4)
                {
                    var meld = new Meld(new List<Tile>
                        {
                            item,
                            item,
                            item,
                            item
                        }, MeldType.Kong, lastDraw.Equals(item)? item: null);

                    output.Add(meld);
                }
            }

            return output;
        }

        public static List<Meld> GetPongableMelds(IEnumerable<Tile> tiles, Tile lastDraw)
        {
            var output = new List<Meld>();
            var count = tiles.Count(q => q.Equals(lastDraw));

            if (count == 2)
            {
                var meld = new Meld(new List<Tile>
                    {
                        lastDraw,
                        lastDraw,
                        lastDraw
                    }, MeldType.Triplet, lastDraw);


                output.Add(meld);
            }


            return output;
        }

        public static List<Meld> GetTriplets(IList<Tile> tiles)
        {
            var tilesWithTripletOccurance = tiles.Distinct().Where(
                        q => tiles.Count(p => p.Equals(q)) >= 3);

            var output = new List<Meld>();

            foreach (var tile in tilesWithTripletOccurance)
            {
                output.Add(new Meld(new List<Tile>
                {
                    tile,
                    tile,
                    tile
                }, MeldType.Triplet));
            }

            return output;
        }

        public static List<Meld> GetKongs(IList<Tile> tiles)
        {
            var tilesWithKongOccurance = tiles.Distinct().Where(
                        q => tiles.Count(p => p.Equals(q)) == 4);

            var output = new List<Meld>();

            foreach (var tile in tilesWithKongOccurance)
            {
                output.Add(new Meld(new List<Tile>
                    {
                        tile, tile, tile, tile
                    }, MeldType.Kong));
            }

            return output;
        }

        public static bool IsBanBanHu(IList<Tile> tiles)
        {
            return tiles.All(q => q.Rank != Rank.Two && q.Rank != Rank.Five && q.Rank != Rank.Eight);
        } 

        public static List<Suit> GetLackSuits(IList<Tile> tiles)
        {
            var suits = tiles.Select(q => q.Suit).Distinct().ToList();
            var output = new List<Suit>();

            if (suits.Count < 3)
            {
                if (!suits.Contains(Suit.Bamboo))
                {
                    output.Add(Suit.Bamboo);
                }
                if (!suits.Contains(Suit.Character))
                {
                    output.Add(Suit.Character);
                }

                if (!suits.Contains(Suit.Dot))
                {
                    output.Add(Suit.Dot);
                }
            }

            return output;
        }

        public static List<Meld> GetChowableMelds(IEnumerable<Tile> tiles, Tile lastDraw)
        {
            var output = new List<Meld>();

            var tilesWithSameSuit = tiles.Where(q => q.Suit == lastDraw.Suit).ToList();



            if (tilesWithSameSuit.Any(q => (int)q.Rank == (int)lastDraw.Rank - 1) &&
                tilesWithSameSuit.Any(q => (int)q.Rank == (int)lastDraw.Rank - 2))
            {
                output.Add(new Meld(new List<Tile>
                {
                    new Tile(lastDraw.Suit, (int) lastDraw.Rank - 2),
                    lastDraw,
                    new Tile(lastDraw.Suit, (int) lastDraw.Rank - 1)
                }, MeldType.Sequence, lastDraw));
            }

            if (tilesWithSameSuit.Any(q => (int)q.Rank == (int)lastDraw.Rank + 1) &&
                tilesWithSameSuit.Any(q => (int)q.Rank == (int)lastDraw.Rank - 1))
            {
                output.Add(new Meld(new List<Tile>
                {
                    new Tile(lastDraw.Suit, (int) lastDraw.Rank - 1),
                    lastDraw,
                    new Tile(lastDraw.Suit, (int) lastDraw.Rank + 1)
                }, MeldType.Sequence, lastDraw));
            }


            if (tilesWithSameSuit.Any(q => (int)q.Rank == (int)lastDraw.Rank + 1) &&
                tilesWithSameSuit.Any(q => (int)q.Rank == (int)lastDraw.Rank + 2))
            {
                output.Add(new Meld(new List<Tile>
                {
                    new Tile(lastDraw.Suit, (int) lastDraw.Rank + 1),
                    lastDraw,
                    new Tile(lastDraw.Suit, (int) lastDraw.Rank + 2)
                }, MeldType.Sequence, lastDraw));
            }

            return output;
        }
    }
}
