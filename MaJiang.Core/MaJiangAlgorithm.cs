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
                var count = list.Count(q => q.GetHashCode() == item.GetHashCode());

                if (count == 4)
                {
                    var meld = new Meld
                    {
                        Type = MeldType.Kong,
                        Tiles = new List<Tile>
                        {
                            item,
                            item,
                            item,
                            item
                        }
                    };

                    if (lastDraw.Equals(item))
                    {
                        meld.LastDraw = item;
                    }

                    output.Add(meld);
                }
            }

            return output;
        }

        public static List<Meld> GetPongableMelds(IEnumerable<Tile> tiles, Tile lastDraw)
        {
            var output = new List<Meld>();
            var count = tiles.Count(q => q.GetHashCode() == lastDraw.GetHashCode());

            if (count == 2)
            {
                var meld = new Meld
                {
                    Type = MeldType.Triplet,
                    Tiles = new List<Tile>
                    {
                        lastDraw,
                        lastDraw,
                        lastDraw
                    },
                    LastDraw = lastDraw
                };


                output.Add(meld);
            }


            return output;
        }

        public static List<Meld> GetChowableMelds(IEnumerable<Tile> tiles, Tile lastDraw)
        {
            var output = new List<Meld>();

            var tilesWithSameSuit = tiles.Where(q => q.Suit.Equals(lastDraw.Suit)).ToList();

            

            if (tilesWithSameSuit.Any(q => q.Rank.Value == lastDraw.Rank.Value - 1) &&
                tilesWithSameSuit.Any(q => q.Rank.Value == lastDraw.Rank.Value - 2))
            {
                output.Add(new Meld
                {
                    Type = MeldType.Sequence,
                    Tiles = new List<Tile>
                    {
                        new Tile(lastDraw.Suit, lastDraw.Rank.Value - 2),
                        lastDraw,
                        new Tile(lastDraw.Suit, lastDraw.Rank.Value - 1)
                    },
                    LastDraw = lastDraw
                });
            }

            if (tilesWithSameSuit.Any(q => q.Rank.Value == lastDraw.Rank.Value + 1) &&
                tilesWithSameSuit.Any(q => q.Rank.Value == lastDraw.Rank.Value - 1))
            {
                output.Add(new Meld
                {
                    Type = MeldType.Sequence,
                    Tiles = new List<Tile>
                    {
                        new Tile(lastDraw.Suit, lastDraw.Rank.Value - 1),
                        lastDraw,
                        new Tile(lastDraw.Suit, lastDraw.Rank.Value + 1)
                    },
                    LastDraw = lastDraw
                });
            }


            if (tilesWithSameSuit.Any(q => q.Rank.Value == lastDraw.Rank.Value + 1) &&
                tilesWithSameSuit.Any(q => q.Rank.Value == lastDraw.Rank.Value + 2))
            {
                output.Add(new Meld
                {
                    Type = MeldType.Sequence,
                    Tiles = new List<Tile>
                    {
                        new Tile(lastDraw.Suit, lastDraw.Rank.Value + 1),
                        lastDraw,
                        new Tile(lastDraw.Suit, lastDraw.Rank.Value + 2)
                    },
                    LastDraw = lastDraw
                });
            }

            return output;
        }
    }
}
