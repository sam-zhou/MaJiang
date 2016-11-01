using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public class NormalWinProcessor: WinProcessor
    {
        public override WinType Type
        {
            get
            {
                return WinType.Normal;
            }
        }

        protected override List<WinningTile> Process()
        {
            var output = new List<WinningTile>();
            foreach (var possibleSet in PossibleSets)
            {
                var ordered = possibleSet.TilesLeft.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();

                var suits = ordered.Select(q => q.Suit).Distinct();

                var dictionary = new Dictionary<Suit, List<MeldCollection>>();

                foreach (var suit in suits)
                {
                    var meldCollections = new List<MeldCollection>();

                    var floor = ordered.Count(q => q.Suit == suit)%3;
                    if (floor == 2 || floor == 0)
                    {
                        var items = GetMeldCollections(new MeldCollection(null, ordered.Where(q => q.Suit == suit).ToList(), possibleSet.Draw)).Where(q => q.Successful);

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

                var result = new List<MeldCollection>();
                foreach (var value in dictionary.Values)
                {
                    result = Join(result, value, possibleSet.Draw);
                }

                foreach (var meldCollection in result)
                {
                    if (meldCollection.Melds.Sum(q => q.TilesCount) == 14)
                    {
                        output.Add(new WinningTile(meldCollection, Type));
                    }
                    
                }
                
            }
            return output;
        }


    }
}
