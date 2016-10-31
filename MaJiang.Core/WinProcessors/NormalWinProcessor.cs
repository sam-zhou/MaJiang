using System.Collections.Generic;
using System.Linq;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public class NormalWinProcessor: WinProcessor
    {
        public NormalWinProcessor(IEnumerable<Tile> tilesOnHand, IEnumerable<Tile> winningDraws)
            : base(tilesOnHand, winningDraws)
        {
        }

        public override IEnumerable<MeldCollection> Validate()
        {
            var output = new List<MeldCollection>();
            foreach (var possibleSet in PossibleSets)
            {
                var ordered = possibleSet.TilesLeft.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();

                var suits = ordered.Select(q => q.Suit).Distinct();

                var dictionary = new Dictionary<Suit, List<MeldCollection>>();

                foreach (var suit in suits)
                {
                    var meldCollections = new List<MeldCollection>();

                    var floor = ordered.Count(q => q.Suit.Equals(suit))%3;
                    if (floor == 2 || floor == 0)
                    {
                        var items =
                                GetMeldCollections(new MeldCollection(null, ordered.Where(q => q.Suit.Equals(suit)).ToList(), possibleSet.Draw)).Where(q => q.Successful);

                        foreach (var meldCollection in items)
                        {
                            meldCollections.Add(meldCollection);
                        }

                        dictionary.Add(suit, meldCollections);
                    }
                    else
                    {
                        return output;
                    }
                }

                if (dictionary.Values.Any(q => !q.Any()))
                {
                    return output;
                }

                var t = dictionary.Values;
            }
            return output;
        }
    }
}
