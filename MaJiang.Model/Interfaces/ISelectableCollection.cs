using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.Interfaces
{
    public interface ISelectableCollection
    {
        Tile GetNextTile();

        Tile GetTileFromLastStack();

        Tile GetTileAt(int index);

        List<Tile> GetNextTiles(int numberOfTiles);

        List<Tile> GetKongTiles();
    }
}
