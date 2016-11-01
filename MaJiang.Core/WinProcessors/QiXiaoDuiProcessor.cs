using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public class QiXiaoDuiProcessor : WinProcessor
    {
        public override WinType Type
        {
            get
            {
                return WinType.QiXiaoDui;
            }
        }

        protected override List<WinningTile> Process()
        {
            var output = new List<WinningTile>();
            foreach (var possibleSet in PossibleSets)
            {
                var ordered = possibleSet.TilesLeft.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();
                var meldCollection = new MeldCollection(null, ordered, possibleSet.Draw);

                var tilesWithEyeOccurance =
                    meldCollection.TilesLeft.Distinct().Count(
                        q => meldCollection.TilesLeft.Count(p => p.Equals(q)) == 2);

                var tilesWithKongOccurance =
                    meldCollection.TilesLeft.Distinct().Count(
                        q => meldCollection.TilesLeft.Count(p => p.Equals(q)) == 4);

                if (tilesWithEyeOccurance + tilesWithKongOccurance*2 == 7)
                {

                    for (int i = 0; i < 7; i++)
                    {
                        var tile = meldCollection.TilesLeft[0];
                        meldCollection.CreateMeld(new Meld
                        {
                            Tiles = new List<Tile>
                            {
                                tile,
                                tile
                            },
                            Type = MeldType.Eye
                        });
                    }
                    output.Add(new WinningTile(meldCollection, tilesWithKongOccurance == 0 ? Type : WinType.LongQiDui, tilesWithKongOccurance));
                }
                
            }

            return output;
        }
    }
}
