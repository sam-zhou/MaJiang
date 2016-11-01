using System.Collections.Generic;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public interface IWinProcessor
    {
        WinType Type { get; }

        IEnumerable<Tile> Draws { get; set; }

        IEnumerable<Tile> TilesOnHand { get; set; }

        List<WinningTile> Validate(IEnumerable<Tile> tilesOnHand, IEnumerable<Tile> draws);
    }
}
