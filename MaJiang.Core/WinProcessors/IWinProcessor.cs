using System.Collections.Generic;
using MaJiang.Model;

namespace MaJiang.Core.WinProcessors
{
    public interface IWinProcessor
    {
        IEnumerable<Tile> Draws { get; set; }



        IEnumerable<Tile> TilesOnHand { get; set; }

        IEnumerable<MeldCollection> Validate();
    }
}
